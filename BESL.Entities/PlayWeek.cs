namespace BESL.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Entities.Infrastructure;
    using static BESL.SharedKernel.GlobalConstants;

    public class PlayWeek : BaseDeletableModel<int>
    {
        public int TournamentTableId { get; set; }
        public TournamentTable TournamentTable { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get => this.StartDate.AddDays(PLAYWEEK_DAYS); }

        public bool IsActive { get; set; }

        public virtual ICollection<Match> MatchFixtures { get; set; } = new HashSet<Match>();
    }
}
