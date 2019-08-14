namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using System.Collections.Generic;

    public class TournamentTablesViewModel
    {
        public int TournamentId { get; set; }

        public IEnumerable<TournamentTableLookupModel> Tables { get; set; }
    }
}
