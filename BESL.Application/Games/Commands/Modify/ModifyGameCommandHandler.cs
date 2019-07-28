namespace BESL.Application.Games.Commands.Modify
{
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;

    public class ModifyGameCommandHandler : IRequestHandler<ModifyGameCommand, Unit>
    {
        private readonly IDeletableEntityRepository<Game> repository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyGameCommandHandler(IDeletableEntityRepository<Game> repository, ICloudinaryHelper cloudinaryHelper)
        {
            this.repository = repository;
            this.cloudinaryHelper = cloudinaryHelper;
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
                var imageUrl = await this.cloudinaryHelper.UploadImage(
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
