namespace BESL.Entities
{
    using System;
    using BESL.Entities.Infrastructure;

    public class PlayerMatch : IDeletableEntity
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}