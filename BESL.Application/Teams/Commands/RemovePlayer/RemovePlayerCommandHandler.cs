namespace BESL.Application.Teams.Commands.RemovePlayer
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, int>
    {
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;

        public RemovePlayerCommandHandler(IDeletableEntityRepository<PlayerTeam> playerTeamsRepository)
        {
            this.playerTeamsRepository = playerTeamsRepository;
        }

        public async Task<int> Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredPlayerTeamEntity = await this.playerTeamsRepository
                .All()
                .Include(pt => pt.Team)
                .FirstOrDefaultAsync(pt => pt.PlayerId == request.PlayerId && pt.TeamId == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(PlayerTeam), null);

            if (desiredPlayerTeamEntity.Team.OwnerId != request.CurrentUserId)
            {
                throw new ForbiddenException();
            }

            this.playerTeamsRepository.Delete(desiredPlayerTeamEntity);
            return await this.playerTeamsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
