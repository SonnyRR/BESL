namespace BESL.Application.Notifications.Queries.GetNotificationsForPlayer
{
    using System.Collections.Generic;

    public class PlayerNotificationsViewModel
    {
        public IEnumerable<PlayerNotificationLookupModel> Notifications { get; set; }
    }
}
