namespace BESL.Application.Games.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class GameCreatedNotification : INotification
    {
        public string GameName { get; set; }

        public class Handler : INotificationHandler<GameCreatedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAcessor userAcessor;            
            public Handler( IDeletableEntityRepository<Notification> notificationRepository, IUserAcessor userAcessor)
            {
                this.notificationRepository = notificationRepository;                
                this.userAcessor = userAcessor;
            }

            public async Task Handle(GameCreatedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = this.userAcessor.UserId,
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
