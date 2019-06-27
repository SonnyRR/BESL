using Microsoft.AspNetCore.Http;

namespace BESL.Application.Games.Queries.ModifyGame
{
    public class ModifyGameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public IFormFile GameImage { get; set; }

    }
}
