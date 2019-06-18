﻿namespace BESL.Domain.Entities
{
    using BESL.Domain.Infrastructure;

    public class CompetitionType : BaseModel<int>
    {
        public string Name { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }
    }
}