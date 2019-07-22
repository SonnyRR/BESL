namespace BESL.Application.Games.Commands.Modify
{
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using BESL.Application.Infrastructure.Cloudinary;

    public class ModifyGameCommandHandler : IRequestHandler<ModifyGameCommand, Unit>
    {
        private readonly IDeletableEntityRepository<Game> repository;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyGameCommandHandler(IDeletableEntityRepository<Game> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
        }

        public async Task<Unit> Handle(ModifyGameCommand request, CancellationToken cancellationToken)
        {
            var existingGame = await this.repository
                .GetByIdWithDeletedAsync(request.Id);

            if (existingGame == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            existingGame.Name = request.Name;
            existingGame.Description = request.Description;

            if (request.GameImage != null)
            {
                var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);
                var imageUrl = await this.cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.GameImage,
                    name: $"{request.Name}-main-shot",
                    transformation: new Transformation().Width(460).Height(215));
                existingGame.GameImageUrl = imageUrl;
            }

            this.repository.Update(existingGame);
            await this.repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
