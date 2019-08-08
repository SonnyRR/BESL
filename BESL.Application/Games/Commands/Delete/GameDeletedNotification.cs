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
            private readonly IUserAcessor userAcessor;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository, IUserAcessor userAcessor)
            {
                this.notificationRepository = notificationRepository;
                this.userAcessor = userAcessor;
            }

            public async Task Handle(GameDeletedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = this.userAcessor.UserId,
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
