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
        private readonly IUserAcessor userAcessor;

        public InvitePlayerCommandHandler(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<TeamInvite> teamInvitesRepository,
            IMediator mediator,
            IUserAcessor userAcessor)
        {
            this.teamsRepository = teamsRepository;
            this.playersRepository = playersRepository;
            this.teamInvitesRepository = teamInvitesRepository;
            this.mediator = mediator;
            this.userAcessor = userAcessor;
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
                .Include(p => p.Claims)
                .SingleOrDefaultAsync(p => p.UserName == request.UserName, cancellationToken)
                ?? throw new PlayerDoesNotExistException(request.UserName);

            if (!await CommonCheckHelper.CheckIfPlayerHasLinkedSteamAccount(desiredPlayer.Id, this.playersRepository))
            {
                throw new PlayerDoesNotHaveALinkedSteamAccountException(desiredPlayer.UserName);
            }

            if (desiredPlayer.Claims.Any(c => c.ClaimType == IS_VAC_BANNED_CLAIM_TYPE))
            {
                throw new PlayerIsVacBannedException(desiredPlayer.UserName);
            }

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                .SingleOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.TeamId);

            if (this.CheckIfUserIsPartOfATeamWithTheSameFormat(desiredPlayer, desiredTeam.TournamentFormatId))
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
                SenderUsername = this.userAcessor.Username
            };

            await this.teamInvitesRepository.AddAsync(invite);
            var rowsAffected = await this.teamInvitesRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new PlayerInvitedNotification
            {
                SenderName = invite.SenderUsername,
                ReceiverId = desiredPlayer.Id,
                TeamName = desiredTeam.Name
            });

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

        private bool CheckIfUserIsPartOfATeamWithTheSameFormat(Player player, int formatId)
        {
            return player.PlayerTeams.Any(x => x.Team.TournamentFormatId == formatId && !x.IsDeleted);
        }

    }
}
