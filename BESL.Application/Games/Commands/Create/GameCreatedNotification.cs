namespace BESL.Application.Games.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using BESL.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class GameCreatedNotification : INotification
    {
        public string GameName { get; set; }

        public class Handler : INotificationHandler<GameCreatedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAccessor userAccessor;            

            public Handler(IDeletableEntityRepository<Notification> notificationRepository, IUserAccessor userAccessor)
            {
                this.notificationRepository = notificationRepository;                
                this.userAccessor = userAccessor;
            }

            public async Task Handle(GameCreatedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = this.userAccessor.UserId,
                    Header = notification.GameName,
                    Content = CREATED_SUCCESSFULLY_MSG,
                    Type = NotificationType.Success,                    
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
