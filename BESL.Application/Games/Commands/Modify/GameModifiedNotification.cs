namespace BESL.Application.Games.Commands.Modify
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class GameModifiedNotification : INotification
    {
        public int Id { get; set; }

        public class Handler : INotificationHandler<GameModifiedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAcessor userAcessor;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository, IUserAcessor userAcessor)
            {
                this.notificationRepository = notificationRepository;
                this.userAcessor = userAcessor;
            }

            public async Task Handle(GameModifiedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = this.userAcessor.UserId,
                    Header = string.Format(NOTIFICATION_ENTITY_HEADER_TEMPLATE_MSG, nameof(Game), notification.Id),
                    Content = DELETED_SUCCESSFULLY_MSG,
                    Type = NotificationType.Success,
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
