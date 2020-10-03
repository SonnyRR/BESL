namespace BESL.Application.Notifications.Queries.GetNotificationsForPlayer
{
    using System;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class PlayerNotificationLookupModel : IMapFrom<Notification>
    {
        public string Id { get; set; }

        public string PlayerId { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
