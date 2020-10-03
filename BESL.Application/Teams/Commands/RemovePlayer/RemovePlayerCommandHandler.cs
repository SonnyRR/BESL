namespace BESL.Application.Teams.Commands.RemovePlayer
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Entities;

    public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, int>
    {
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;
        private readonly IUserAccessor userAccessor;

        public RemovePlayerCommandHandler(IDeletableEntityRepository<PlayerTeam> playerTeamsRepository, IUserAccessor userAccessor)
        {
            this.playerTeamsRepository = playerTeamsRepository;
            this.userAccessor = userAccessor;
        }

        public async Task<int> Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredPlayerTeamEntity = await this.playerTeamsRepository
                .All()
                .Include(pt => pt.Team)
                .FirstOrDefaultAsync(pt => pt.PlayerId == request.PlayerId && pt.TeamId == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(PlayerTeam), null);

            var teamPlayersCount = await this.playerTeamsRepository
                .AllAsNoTracking()
                .CountAsync(pt => pt.TeamId == request.TeamId);

            if (desiredPlayerTeamEntity.Team.OwnerId != this.userAccessor.UserId)
            {
                if (desiredPlayerTeamEntity.PlayerId != this.userAccessor.UserId)
                {
                    throw new ForbiddenException();
                }
            }

            if (desiredPlayerTeamEntity.Team.OwnerId == request.PlayerId
                && teamPlayersCount != 1)
            {
                throw new OwnerOfAMustTransferOwnerShipBeforeLeavingTeamException();
            }

            if (desiredPlayerTeamEntity.Team.OwnerId == this.userAccessor.UserId)
            {
                desiredPlayerTeamEntity.Team.OwnerId = null;
                this.playerTeamsRepository.Update(desiredPlayerTeamEntity);
            }

            this.playerTeamsRepository.Delete(desiredPlayerTeamEntity);
            return await this.playerTeamsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
