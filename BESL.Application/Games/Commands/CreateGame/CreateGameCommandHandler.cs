namespace BESL.Application.Games.Commands.CreateGame
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Common;
    using BESL.Domain.Entities;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IConfiguration configuration;

        public CreateGameCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var cloudinary = CloudinaryHelper.GetInstance(this.configuration);

            var url = await CloudinaryHelper.UploadImage(cloudinary, request.GameImage, $"{request.Name}-main-shot");

            Game game = new Game()
            {
                Name = request.Name,
                Description = request.Description,
            };

            this.context.Games.Add(game);
            await this.context.SaveChangesAsync(cancellationToken);

            return game.Id;
        }
    }
}
