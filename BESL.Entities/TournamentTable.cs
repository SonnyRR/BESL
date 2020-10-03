namespace BESL.Entities
{
    using System.Collections.Generic;
    using BESL.Entities.Infrastructure;

    public class TournamentTable : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int MaxNumberOfTeams { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public virtual ICollection<TeamTableResult> TeamTableResults { get; set; } = new HashSet<TeamTableResult>();
        public virtual ICollection<PlayWeek> PlayWeeks { get; set; } = new HashSet<PlayWeek>();
    }
}
