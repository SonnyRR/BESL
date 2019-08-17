namespace BESL.Application.Tournaments.Queries.GetTournamentsForGame
{
    using System.Collections.Generic;

    public class GameTournamentsViewModel
    {
        public IEnumerable<TournamentLookupModel> Tournaments { get; set; }
    }
}
