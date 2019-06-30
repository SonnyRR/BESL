namespace BESL.Application.Formats.Queries.Create
{
    using System.Collections.Generic;
    public class CreateTournamentFormatViewModel
    {
        public string Name { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }

        public IEnumerable<GameLookupModel> Games { get; set; }
    }
}
