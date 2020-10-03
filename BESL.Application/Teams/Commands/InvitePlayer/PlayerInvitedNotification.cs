namespace BESL.Application.Teams.Commands.InvitePlayer
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using BESL.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class PlayerInvitedNotification : INotification
    {
        public string SenderName { get; set; }

        public string TeamName { get; set; }

        public string ReceiverId { get; set; }

        public class Handler : INotificationHandler<PlayerInvitedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository)
            {
                this.notificationRepository = notificationRepository;
            }

            public async Task Handle(PlayerInvitedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = notification.ReceiverId,
                    Header = TEAM_INVITE_HEADER_MSG,
                    Content = string.Format(TEAM_INVITE_TEMPLATE_MSG, notification.SenderName, notification.TeamName),
                    Type = NotificationType.Success
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
