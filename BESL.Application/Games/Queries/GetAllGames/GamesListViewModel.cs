namespace BESL.Application.Games.Queries.GetAllGames
{
    using System.Collections.Generic;

    public class GamesListViewModel
    {
        public ICollection<GameLookupModel> Games { get; set; }
    }
}
