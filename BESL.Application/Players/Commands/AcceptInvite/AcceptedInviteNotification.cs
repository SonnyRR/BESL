namespace BESL.Application.Players.Commands.AcceptInvite
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;

    public class AcceptedInviteNotification : INotification
    {
        public string PlayerId { get; set; }

        public string TeamName { get; set; }

        public class Handler : INotificationHandler<AcceptedInviteNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository)
            {
                this.notificationRepository = notificationRepository;
            }

            public async Task Handle(AcceptedInviteNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = notification.PlayerId,
                    Header = notification.TeamName,
                    Content = $"You have successfully joined {notification.TeamName}!",
                    Type = NotificationType.Success
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
