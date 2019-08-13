namespace BESL.Application.Games.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class GameDeletedNotification : INotification
    {
        public string GameName { get; set; }

        public class Handler : INotificationHandler<GameDeletedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAccessor userAccessor;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository, IUserAccessor userAccessor)
            {
                this.notificationRepository = notificationRepository;
                this.userAccessor = userAccessor;
            }

            public async Task Handle(GameDeletedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = this.userAccessor.UserId,
                    Header = notification.GameName,
                    Content = DELETED_SUCCESSFULLY_MSG,
                    Type = NotificationType.Success,
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
