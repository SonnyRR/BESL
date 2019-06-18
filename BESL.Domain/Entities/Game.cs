namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Game : BaseModel<int>
    {
        public Game()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; } = new HashSet<Competition>();        
    }
}