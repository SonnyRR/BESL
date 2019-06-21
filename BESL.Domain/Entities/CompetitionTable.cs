namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class CompetitionTable : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int MaxNumberOfTeams { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public virtual ICollection<Team> SignedUpTeams { get; set; } = new HashSet<Team>();
        public virtual ICollection<TeamTableResult> CompetitionTableResults { get; set; } = new HashSet<TeamTableResult>();
    }
}
