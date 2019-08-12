namespace BESL.Application.Common.Models.Lookups
{
    using System;
    using System.Collections.Generic;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class PlayWeekLookupModel : IMapFrom<PlayWeek>
    {
        public int Id  { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TournamentTableId { get; set; }
    }
}
