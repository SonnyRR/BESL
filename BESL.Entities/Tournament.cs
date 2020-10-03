namespace BESL.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Entities.Infrastructure;

    public class Tournament : BaseDeletableModel<int>
    { 
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool AreSignupsOpen { get; set; }

        public string TournamentImageUrl { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int FormatId { get; set; }
        public TournamentFormat Format { get; set; }

        public virtual ICollection<TournamentTable> Tables { get; set; } = new HashSet<TournamentTable>();
    }
}
