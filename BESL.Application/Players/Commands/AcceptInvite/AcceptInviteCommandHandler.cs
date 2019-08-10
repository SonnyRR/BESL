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
    using System.Linq;

    public class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamInvite> teamInvitesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;
        private readonly IMediator mediator;

        public AcceptInviteCommandHandler(
            IDeletableEntityRepository<TeamInvite> teamInvitesRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<PlayerTeam> playerTeamsRepository,
            IMediator mediator)
        {
            this.teamInvitesRepository = teamInvitesRepository;
            this.teamsRepository = teamsRepository;
            this.playerTeamsRepository = playerTeamsRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredInvite = await this.teamInvitesRepository
                .All()
                .SingleOrDefaultAsync(ti => ti.Id == request.InviteId, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamInvite), request.InviteId);

            if (desiredInvite.PlayerId != request.UserId)
            {
                throw new ForbiddenException();
            }

            var desiredTeam = await this.teamsRepository
                .All()
                .Include(t => t.PlayerTeams)
                .SingleOrDefaultAsync(t => t.Id == desiredInvite.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), desiredInvite.TeamId);

            if (desiredTeam.PlayerTeams.Count >= TEAM_MAX_BACKUP_PLAYERS_COUNT)
            {
                throw new TeamIsFullException(desiredTeam.Name);
            }

            if (this.CheckIfPlayerParticipatesInATeamWithTheSameFormat(desiredTeam.TournamentFormatId, request.UserId))
            {
                throw new PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException();
            }

            desiredTeam.PlayerTeams.Add(new PlayerTeam { PlayerId = request.UserId });
            var affectedRows = await this.teamsRepository.SaveChangesAsync(cancellationToken);

            await this.teamInvitesRepository.SaveChangesAsync(cancellationToken);
            this.teamInvitesRepository.Delete(desiredInvite);

            await this.mediator.Publish(new AcceptedInviteNotification() { PlayerId = request.UserId, TeamName = desiredTeam.Name });

            return affectedRows;
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
