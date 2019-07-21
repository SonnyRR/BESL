namespace BESL.Application.Games.Queries.ModifyGame
{
    using Microsoft.AspNetCore.Http;

    public class ModifyGameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public IFormFile GameImage { get; set; }
    }
}
