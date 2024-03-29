﻿namespace BESL.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Entities.Infrastructure;

    public class Match : BaseDeletableModel<int>
    {
        public int PlayWeekId { get; set; }
        public PlayWeek PlayWeek { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public int? WinnerTeamId { get; set; }
        public Team WinnerTeam { get; set; }

        public bool IsDraw { get; set; }

        public bool IsResultConfirmed { get; set; }

        public DateTime ScheduledDate { get; set; }

        public virtual ICollection<PlayerMatch> ParticipatedPlayers { get; set; } = new HashSet<PlayerMatch>();
    }
}