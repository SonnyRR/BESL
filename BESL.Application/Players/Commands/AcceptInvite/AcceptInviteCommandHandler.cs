namespace BESL.Application.Players.Commands.AcceptInvite
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamInvite> teamInvitesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IMediator mediator;

        public AcceptInviteCommandHandler(
            IDeletableEntityRepository<TeamInvite> teamInvitesRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            IMediator mediator)
        {
            this.teamInvitesRepository = teamInvitesRepository;
            this.teamsRepository = teamsRepository;
            this.playersRepository = playersRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredPlayer = await this.playersRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == request.UserId)
                ?? throw new NotFoundException(nameof(Player), request.UserId);

            var desiredInvite = await this.teamInvitesRepository
                .All()
                .SingleOrDefaultAsync(ti => ti.Id == request.InviteId, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamInvite), request.InviteId);

            var desiredTeam = await this.teamsRepository
                .All()
                .Include(t => t.PlayerTeams)
                .SingleOrDefaultAsync(t => t.Id == desiredInvite.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), desiredInvite.TeamId);

            if (desiredTeam.PlayerTeams.Count >= TEAM_MAX_BACKUP_PLAYERS_COUNT)
            {
                throw new TeamIsFullException(desiredTeam.Name);
            }

            desiredTeam.PlayerTeams.Add(new PlayerTeam { PlayerId = desiredPlayer.Id, TeamId = desiredTeam.Id });
            this.teamsRepository.Update(desiredTeam);
            this.teamInvitesRepository.Delete(desiredInvite);
            var affectedRows = await this.teamsRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new AcceptedInviteNotification() { PlayerId = desiredPlayer.Id, TeamName = desiredTeam.Name });

            return affectedRows;
        }
    }
}
