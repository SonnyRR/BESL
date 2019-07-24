namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class TournamentTable : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int MaxNumberOfTeams { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public virtual ICollection<TeamTableResult> TeamTableResults { get; set; } = new HashSet<TeamTableResult>();
    }
}
