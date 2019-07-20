namespace BESL.Domain.Entities
{
    using System;

    using BESL.Domain.Infrastructure;

    public class PlayerTeam : IDeletableEntity, IAuditInfo
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
