﻿namespace BESL.Application.Games.Commands.Modify
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using BESL.Entities.Enums;
    using static BESL.SharedKernel.GlobalConstants;

    public class GameModifiedNotification : INotification
    {
        public string GameName { get; set; }

        public class Handler : INotificationHandler<GameModifiedNotification>
        {
            private readonly IDeletableEntityRepository<Notification> notificationRepository;
            private readonly IUserAccessor userAccessor;

            public Handler(IDeletableEntityRepository<Notification> notificationRepository, IUserAccessor userAccessor)
            {
                this.notificationRepository = notificationRepository;
                this.userAccessor = userAccessor;
            }

            public async Task Handle(GameModifiedNotification notification, CancellationToken cancellationToken)
            {
                var notificationEntity = new Notification()
                {
                    PlayerId = this.userAccessor.UserId,
                    Header = string.Format(NOTIFICATION_ENTITY_HEADER_TEMPLATE_MSG, nameof(Game), notification.GameName),
                    Content = MODIFIED_SUCCESSFULLY_MSG,
                    Type = NotificationType.Success,
                };

                await this.notificationRepository.AddAsync(notificationEntity);
                await this.notificationRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
