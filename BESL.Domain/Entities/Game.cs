namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Game : BaseModel<int>
    {
        public Game()
        {
            base.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public virtual ICollection<CompetitionType> CompetitionTypes { get; set; } = new HashSet<CompetitionType>();        
    }
}