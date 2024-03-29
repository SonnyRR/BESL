﻿namespace BESL.Entities
{
    using System.Collections.Generic;
    using BESL.Entities.Infrastructure;

    public class Team : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string ImageUrl { get; set; }

        public string OwnerId { get; set; }
        public Player Owner { get; set; }

        public int TournamentFormatId { get; set; }
        public TournamentFormat TournamentFormat { get; set; }           

        public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new HashSet<PlayerTeam>();
        public virtual ICollection<TeamTableResult> TeamTableResults { get; set; } = new HashSet<TeamTableResult>();        
    }
}