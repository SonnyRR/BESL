namespace BESL.Application.Games.Commands.CreateGame
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateGameCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile GameImage { get; set; }
    }
}
