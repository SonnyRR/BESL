namespace BESL.Application.Matches.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, int>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<PlayWeek> playWeeksRepository;

        public CreateMatchCommandHandler(
            IDeletableEntityRepository<Match> matchesRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<PlayWeek> playWeeksRepository)
        {
            this.matchesRepository = matchesRepository;
            this.teamsRepository = teamsRepository;
            this.playWeeksRepository = playWeeksRepository;
        }

        public async Task<int> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfTeamExists(request.HomeTeamId, this.teamsRepository))
            {
                throw new NotFoundException(nameof(Team), request.HomeTeamId);
            }

            if (!await CommonCheckHelper.CheckIfTeamExists(request.AwayTeamId, this.teamsRepository))
            {
                throw new NotFoundException(nameof(Team), request.AwayTeamId);
            }

            if (await this.CheckIfTeamIsDropped(request.HomeTeamId, request.PlayWeekId)
                || await this.CheckIfTeamIsDropped(request.AwayTeamId, request.PlayWeekId))
            {
                throw new ForbiddenException();
            }

            var match = new Match
            {
                HomeTeamId = request.HomeTeamId,
                AwayTeamId = request.AwayTeamId,
                ScheduledDate = request.PlayDate,
                PlayWeekId = request.PlayWeekId
            };

            await this.matchesRepository.AddAsync(match);
            await this.matchesRepository.SaveChangesAsync(cancellationToken);

            return match.Id;
        }

        private async Task<bool> CheckIfTeamIsDropped(int teamId, int playWeekId)
        {
            var ttr = await this.playWeeksRepository
                .AllAsNoTracking()
                .Where(x => x.Id == playWeekId)
                .Include(x => x.TournamentTable)
                    .ThenInclude(x => x.TeamTableResults)
                .SelectMany(x => x.TournamentTable.TeamTableResults)
                .SingleOrDefaultAsync(x => x.TeamId == teamId);

            return ttr.IsDropped;
        }
    }
}
