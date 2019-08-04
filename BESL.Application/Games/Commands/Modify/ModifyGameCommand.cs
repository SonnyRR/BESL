namespace BESL.Application.Games.Commands.Modify
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class ModifyGameCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public IFormFile GameImage { get; set; }
    }
}
