namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Game : BaseModel<int>
    {        
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();     
    }
}