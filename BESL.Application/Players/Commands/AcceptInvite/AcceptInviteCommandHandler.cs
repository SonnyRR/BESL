namespace BESL.Application.Players.Commands.AcceptInvite
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.AddPlayer;
    using BESL.Domain.Entities;

    public class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamInvite> teamInvitesRepository;
        private readonly IMediator mediator;
        private readonly IUserAccessor userAccessor;

        public AcceptInviteCommandHandler(
            IDeletableEntityRepository<TeamInvite> teamInvitesRepository,
            IMediator mediator,
            IUserAccessor userAccessor)
        {
            this.teamInvitesRepository = teamInvitesRepository;
            this.mediator = mediator;
            this.userAccessor = userAccessor;
        }

        public async Task<int> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentUserId = this.userAccessor.UserId;

            var desiredInvite = await this.teamInvitesRepository
                .All()
                .SingleOrDefaultAsync(ti => ti.Id == request.InviteId, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamInvite), request.InviteId);

            if (desiredInvite.PlayerId != currentUserId)
            {
                throw new ForbiddenException();
            }

            await this.mediator.Send(new AddPlayerCommand { PlayerId = desiredInvite.PlayerId, TeamId = desiredInvite.TeamId });

            this.teamInvitesRepository.Delete(desiredInvite);
            var affectedRows = await this.teamInvitesRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new AcceptedInviteNotification() { PlayerId = currentUserId, TeamName = desiredInvite.TeamName });

            return affectedRows;
        }
    }
}
