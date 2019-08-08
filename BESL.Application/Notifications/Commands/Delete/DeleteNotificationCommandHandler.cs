namespace BESL.Application.Notifications.Commands.Delete
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

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, int>
    {
        private readonly IDeletableEntityRepository<Notification> notificationsRepository;
        private readonly IUserAcessor userAcessor;

        public DeleteNotificationCommandHandler(IDeletableEntityRepository<Notification> notificationsRepository, IUserAcessor userAcessor)
        {
            this.notificationsRepository = notificationsRepository;
            this.userAcessor = userAcessor;
        }

        public async Task<int> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredNotification = await this.notificationsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(n => n.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Notification), request.Id);

            if (desiredNotification.PlayerId != this.userAcessor.UserId)
            {
                throw new DeleteFailureException(nameof(Notification), desiredNotification.Id, PLAYER_CAN_ONLY_DELETE_HIS_OWN_NOTIFICATIONS_MSG);
            }

            this.notificationsRepository.Delete(desiredNotification);
            return await this.notificationsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
