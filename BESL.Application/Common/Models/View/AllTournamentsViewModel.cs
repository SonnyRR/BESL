namespace BESL.Application.Common.Models.View
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class AllTournamentsViewModel
    {
        public IEnumerable<TournamentLookupModel> Tournaments { get; set; }
    }
}
