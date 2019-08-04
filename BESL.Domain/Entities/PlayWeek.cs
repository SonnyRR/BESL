namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using BESL.Domain.Infrastructure;

    public class PlayWeek : BaseDeletableModel<int>
    {
        public int TournamentTableId { get; set; }
        public TournamentTable TournamentTable { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get => this.StartDate.AddDays(7); }

        public virtual ICollection<Match> MatchFixtures { get; set; } = new HashSet<Match>();
    }
}
