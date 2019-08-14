﻿namespace BESL.Domain.Entities
{
    using BESL.Domain.Infrastructure;

    public class TeamTableResult : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int TournamentTableId { get; set; }
        public TournamentTable TournamentTable { get; set; }

        public bool IsDropped { get; set; }

        public int TotalPoints { get; set; }

        public int PenaltyPoints { get; set; }
    }
}
