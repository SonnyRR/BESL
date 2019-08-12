namespace BESL.Application.Matches.Queries.GetMatchesForPlayWeek
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class MatchesForPlayWeekViewModel
    {
        public int PlayWeekId { get; set; }

        public IEnumerable<MatchLookupModel> Matches { get; set; }
    }
}
