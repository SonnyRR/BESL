namespace BESL.Application.TournamentTables.Queries.GetTeamsForTournamentTable
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class TeamsForTournamentTableViewModel
    {
        public IEnumerable<TeamsSelectItemLookupModel> Players { get; set; }
    }
}
