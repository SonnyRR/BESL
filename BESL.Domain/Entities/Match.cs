namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Match : BaseDeletableModel<int>
    {
        public int CompetitionTableResultId { get; set; }
        public TeamTableResult CompetitionTableResult { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public int WinnerTeamId { get; set; }
        public Team WinnerTeam { get; set; }

        public bool IsDraw { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public virtual ICollection<PlayerMatch> ParticipatedPlayers { get; set; } = new HashSet<PlayerMatch>();
    }
}