namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Game : BaseDeletableModel<int>
    {
        public Game()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; } = new HashSet<Tournament>();
        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}