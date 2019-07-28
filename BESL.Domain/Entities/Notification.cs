namespace BESL.Domain.Entities
{
    using BESL.Domain.Entities.Enums;
    using BESL.Domain.Infrastructure;

    public class Notification : BaseDeletableModel<string>
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }
    }
}
