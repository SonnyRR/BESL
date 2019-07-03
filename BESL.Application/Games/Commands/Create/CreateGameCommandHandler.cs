﻿namespace BESL.Application.Games.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Common;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public CreateGameCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

            var url = await this.cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.GameImage,
                    name: $"{request.Name}-main-shot"
                    // TODO: set picture transformation size
                    //transformation: new Transformation().Width(500).Height(500)
                ); 

            Game game = new Game()
            {
                Name = request.Name,
                Description = request.Description,
                GameImageUrl = url
            };

            this.context.Games.Add(game);
            await this.context.SaveChangesAsync(cancellationToken);

            return game.Id;
        }
    }
}
