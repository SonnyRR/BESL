namespace BESL.Application.Matches.Commands.EditMatchResult
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class EditMatchResultCommandHandler : IRequestHandler<EditMatchResultCommand, int>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IUserAccessor userAccessor;

        public EditMatchResultCommandHandler(IDeletableEntityRepository<Match> matchesRepository, IUserAccessor userAccessor)
        {
            this.matchesRepository = matchesRepository;
            this.userAccessor = userAccessor;
        }

        public async Task<int> Handle(EditMatchResultCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .All()
                .Include(m => m.HomeTeam)
                    .ThenInclude(ht => ht.PlayerTeams)
                .Include(m => m.AwayTeam)
                    .ThenInclude(at => at.PlayerTeams)
                .Include(m => m.ParticipatedPlayers)
                .Include(m => m.PlayWeek)
                    .ThenInclude(pw => pw.TournamentTable)
                        .ThenInclude(pw => pw.Tournament)
                            .ThenInclude(pw => pw.Format)
                .SingleOrDefaultAsync(m => m.Id == request.Id)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            if (desiredMatch.HomeTeam.OwnerId != this.userAccessor.UserId
                && desiredMatch.AwayTeam.OwnerId != this.userAccessor.UserId)
            {
                throw new ForbiddenException();
            }

            desiredMatch.HomeTeamScore = request.HomeTeamScore;
            desiredMatch.AwayTeamScore = request.AwayTeamScore;
            desiredMatch.WinnerTeamId = desiredMatch.HomeTeamScore > desiredMatch.AwayTeamScore
                ? desiredMatch.HomeTeamId
                : desiredMatch.AwayTeamId;

            if (desiredMatch.HomeTeamScore == desiredMatch.AwayTeamScore)
            {
                desiredMatch.IsDraw = true;
            }

            if (request.ParticipatedPlayersIds != null)
            {
                var participatedPlayersIdsDistinct = request.ParticipatedPlayersIds.Distinct();

                if (participatedPlayersIdsDistinct.Count() < desiredMatch.PlayWeek.TournamentTable.Tournament.Format.TotalPlayersCount)
                {
                    throw new InvalidMatchParticipantsCountException(desiredMatch.PlayWeek.TournamentTable.Tournament.Format.TotalPlayersCount);
                }

                foreach (var playerId in participatedPlayersIdsDistinct)
                {
                    if (!desiredMatch.HomeTeam.PlayerTeams.Any(pt => pt.PlayerId == playerId)
                        && !desiredMatch.AwayTeam.PlayerTeams.Any(at => at.PlayerId == playerId))
                    {
                        throw new PlayerIsNotAMemberOfTeamException(desiredMatch.HomeTeam.Name, desiredMatch.AwayTeam.Name);
                    }

                    if (desiredMatch.ParticipatedPlayers.Any(x => x.PlayerId == playerId))
                    {
                        continue;
                    }

                    desiredMatch.ParticipatedPlayers.Add(new PlayerMatch { PlayerId = playerId, MatchId = desiredMatch.Id });
                }
            }

            this.matchesRepository.Update(desiredMatch);
            return await this.matchesRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
