namespace BESL.Application.Formats.Commands
{
    public class CreateTournamentFormatCommand
    {
        public string Name { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }
    }
}
