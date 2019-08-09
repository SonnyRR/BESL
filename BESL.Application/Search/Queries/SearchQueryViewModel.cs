using System;
using System.Collections.Generic;
using System.Text;

namespace BESL.Application.Search.Queries
{
    public class SearchQueryViewModel
    {
        public string SearchQuery { get; set; }

        public IEnumerable<PlayerLookupModel> Players { get; set; }

        public IEnumerable<TeamLookupModel> Teams { get; set; }

        public IEnumerable<TournamentLookupModel> Tournaments { get; set; }
    }
}
