namespace BESL.Application.Games.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMediator mediator;

        public CreateGameCommandHandler(
            IDeletableEntityRepository<Game> gamesRepository,
            ICloudinaryHelper cloudinaryHelper,
            IMediator mediator)
        {
            this.gamesRepository = gamesRepository;
            this.cloudinaryHelper = cloudinaryHelper;
            this.mediator = mediator;
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            Game game = new Game()
            {
                Name = request.Name,
                Description = request.Description,
                GameImageUrl = await this.UploadGameImage(request)
            };

            await this.gamesRepository.AddAsync(game);
            await this.gamesRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new GameCreatedNotification() { GameName = game.Name });

            return game.Id;
        }

        private async Task<string> UploadGameImage(CreateGameCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                request.GameImage,
                name: $"{request.Name}-main-shot",
                transformation: new Transformation().Width(GAME_IMAGE_WIDTH).Height(GAME_IMAGE_HEIGHT));
        }
    }
}
