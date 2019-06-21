﻿namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Team : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string OwnerId { get; set; }
        public Player Owner { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
            
        public int? CurrentActiveCompetitionTableId { get; set; }
        public CompetitionTable CurrentActiveCompetitionTable { get; set; }

        public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new HashSet<PlayerTeam>();
        public virtual ICollection<TeamTableResult> PreviousCompetitionTablesResults { get; set; } = new HashSet<TeamTableResult>();
        
    }
}