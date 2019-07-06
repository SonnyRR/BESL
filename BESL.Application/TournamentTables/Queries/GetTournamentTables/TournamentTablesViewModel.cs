namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using System.Collections.Generic;

    public class TournamentTablesViewModel
    {
        public IEnumerable<TournamentTableLookupModel> Tables { get; set; }
    }
}
