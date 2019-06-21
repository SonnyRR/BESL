namespace BESL.Application.Games.Queries.GetAllGames
{
    public class GameLookupModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CurrentActiveTournaments { get; set; }

        public int RegisteredTeams { get; set; }
    }
}
