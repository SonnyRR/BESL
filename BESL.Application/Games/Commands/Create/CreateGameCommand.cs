namespace BESL.Application.Games.Commands.Create
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateGameCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile GameImage { get; set; }
    }
}
