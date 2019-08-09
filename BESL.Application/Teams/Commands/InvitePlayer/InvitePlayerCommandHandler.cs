namespace BESL.Application.Teams.Commands.InvitePlayer
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
    using static BESL.Common.GlobalConstants;

    public class InvitePlayerCommandHandler : IRequestHandler<InvitePlayerCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<TeamInvite> teamInvitesRepository;
        private readonly IMediator mediator;

        public InvitePlayerCommandHandler(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<TeamInvite> teamInvitesRepository,
            IMediator mediator)
        {
            this.teamsRepository = teamsRepository;
            this.playersRepository = playersRepository;
            this.teamInvitesRepository = teamInvitesRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(InvitePlayerCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                throw new InvalidSearchQueryException();
            }

            var desiredPlayer = await this.playersRepository
                .AllAsNoTracking()
                .Include(p => p.PlayerTeams)
                    .ThenInclude(pt => pt.Team)
                .SingleOrDefaultAsync(p => p.UserName == request.UserName, cancellationToken)
                ?? throw new PlayerDoesNotExistException(request.UserName);

            if (!await CommonCheckHelper.CheckIfUserHasLinkedSteamAccount(desiredPlayer.Id, this.playersRepository))
            {
                throw new PlayerDoesNotHaveALinkedSteamAccountException(desiredPlayer.UserName);
            }

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                .SingleOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.TeamId);

            var playerTeams = desiredPlayer
                .PlayerTeams
                .Select(x => x.Team)
                .ToList();

            if (playerTeams.Any(x => x.TournamentFormatId == desiredTeam.TournamentFormatId))
            {
                throw new PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException(request.UserName);
            }

            if (desiredTeam.PlayerTeams.Count >= desiredTeam.TournamentFormat.TeamPlayersCount + TEAM_MAX_BACKUP_PLAYERS_COUNT)
            {
                throw new TeamIsFullException(desiredTeam.Name);
            }

            if (await this.CheckIfUserAlreadyHasAnInviteForGivenTeam(request))
            {
                throw new PlayerAlreadyHasPendingInvite(request.UserName);
            }


            var invite = new TeamInvite
            {
                PlayerId = desiredPlayer.Id,
                TeamId = desiredTeam.Id,
                TeamName = desiredTeam.Name,
                SenderUsername = request.SenderUsername
            };

            await this.teamInvitesRepository.AddAsync(invite);
            var rowsAffected = await this.teamInvitesRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new PlayerInvitedNotification { SenderName = request.SenderUsername, ReceiverId = desiredPlayer.Id, TeamName = desiredTeam.Name});

            return rowsAffected;
        }

        private async Task<bool> CheckIfUserAlreadyHasAnInviteForGivenTeam(InvitePlayerCommand request)
        {
            return await this.playersRepository
                .AllAsNoTracking()
                .Where(p => p.UserName == request.UserName)
                .SelectMany(p => p.Invites.Where(i => i.IsDeleted == false))
                .AnyAsync(i => i.TeamId == request.TeamId);
        }
    }
}
