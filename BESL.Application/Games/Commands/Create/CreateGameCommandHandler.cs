namespace BESL.Application.Games.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Application.Infrastructure.Cloudinary;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;
    using BESL.Application.Exceptions;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IDeletableEntityRepository<Game> repository;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public CreateGameCommandHandler(IDeletableEntityRepository<Game> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {            
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

            var url = await this.cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.GameImage,
                    name: $"{request.Name}-main-shot",
                    transformation: new Transformation().Width(GAME_IMAGE_WIDTH).Height(GAME_IMAGE_HEIGHT)); 

            Game game = new Game()
            {
                Name = request.Name,
                Description = request.Description,
                GameImageUrl = url
            };

            await this.repository.AddAsync(game);
            await this.repository.SaveChangesAsync(cancellationToken);

            return game.Id;
        }
    }
}
