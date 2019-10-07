namespace BESL.Application.Search.Queries.QuerySearch
{
    using System.Collections.Generic;
    
    public class SearchQueryViewModel
    {
        public string SearchQuery { get; set; }

        public IEnumerable<PlayerLookupModel> Players { get; set; }

        public IEnumerable<TeamLookupModel> Teams { get; set; }

        public IEnumerable<TournamentLookupModel> Tournaments { get; set; }
    }
}
