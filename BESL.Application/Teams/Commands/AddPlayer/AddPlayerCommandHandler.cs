namespace BESL.Application.Teams.Commands.AddPlayer
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

    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;

        public AddPlayerCommandHandler(IDeletableEntityRepository<Team> teamsRepository, IDeletableEntityRepository<PlayerTeam> playerTeamsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.playerTeamsRepository = playerTeamsRepository;
        }

        public async Task<int> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamsRepository
                .All()
                .Include(t => t.PlayerTeams)
                .SingleOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.TeamId);

            if (await CommonCheckHelper.CheckIfTeamIsFull(desiredTeam.Id, this.teamsRepository))
            {
                throw new TeamIsFullException(desiredTeam.Name);
            }

            if (this.CheckIfPlayerParticipatesInATeamWithTheSameFormat(desiredTeam.TournamentFormatId, request.PlayerId))
            {
                throw new PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException();
            }

            desiredTeam.PlayerTeams.Add(new PlayerTeam { PlayerId = request.PlayerId });
            return await this.teamsRepository.SaveChangesAsync(cancellationToken);
        }

        private bool CheckIfPlayerParticipatesInATeamWithTheSameFormat(int formatId, string playerId)
        {
            return this.playerTeamsRepository
                .AllAsNoTracking()
                .Where(pt => pt.PlayerId == playerId)
                .Include(pt => pt.Team)
                .Any(pt => pt.Team.TournamentFormatId == formatId);
        }
    }
}
