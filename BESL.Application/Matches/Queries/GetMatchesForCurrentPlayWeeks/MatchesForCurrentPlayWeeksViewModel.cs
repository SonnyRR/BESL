namespace BESL.Application.Matches.Queries.GetMatchesForCurrentPlayWeeks
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class MatchesForCurrentPlayWeeksViewModel
    {
        public IEnumerable<MatchLookupModel> Matches { get; set; }
    }
}
