using System.Collections.Generic;

namespace BESL.Application.Formats.Queries.GetAllGames
{
    public class GetAllGamesListViewModel
    {
        public IEnumerable<GameLookupModel> Games { get; set; }
    }
}
