namespace BESL.Application.Games.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Common.Cloudinary;
    using BESL.Domain.Entities;
    using CloudinaryDotNet;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IRepository<Game> repository;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public CreateGameCommandHandler(IRepository<Game> repository, IConfiguration configuration)
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
                    transformation: new Transformation().Width(460).Height(215)
                ); 

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
