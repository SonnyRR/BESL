namespace BESL.Application.TournamentFormats.Queries.Create
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class CreateTournamentFormatViewModel
    {
        public string Name { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }

        public IEnumerable<GameSelectItemLookupModel> Games { get; set; }
    }
}
