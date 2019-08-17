namespace BESL.Application.Tournaments.Queries.Common
{
    using System.Collections.Generic;

    public class AllTournamentsViewModel
    {
        public IEnumerable<TournamentLookupModel> Tournaments { get; set; }
    }
}
