namespace BESL.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Entities.Infrastructure;

    public class Game : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public virtual ICollection<TournamentFormat> TournamentFormats { get; set; } = new HashSet<TournamentFormat>();
    }
}