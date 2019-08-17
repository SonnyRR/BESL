namespace BESL.Application.Tournaments.Queries.GetAllTournaments
{
    using System.Collections.Generic;

    public class AllTournamentsViewModel
    {
        public IEnumerable<TournamentLookupModel> Tournaments { get; set; }
    }
}
