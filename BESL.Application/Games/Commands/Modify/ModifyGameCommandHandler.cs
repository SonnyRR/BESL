namespace BESL.Application.Games.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class ModifyGameCommandHandler : IRequestHandler<ModifyGameCommand, int>
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyGameCommandHandler(IDeletableEntityRepository<Game> gamesRepository, ICloudinaryHelper cloudinaryHelper)
        {
            this.gamesRepository = gamesRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(ModifyGameCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var existingGame = await this.gamesRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Game), request.Id);

            existingGame.Name = request.Name;
            existingGame.Description = request.Description;

            if (request.GameImage != null)
            { 
                existingGame.GameImageUrl = await this.UploadGameImage(request);
            }

            this.gamesRepository.Update(existingGame);
            return await this.gamesRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task<string> UploadGameImage(ModifyGameCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                request.GameImage,
                name: $"{request.Name}-main-shot",
                transformation: new Transformation().Width(GAME_IMAGE_WIDTH).Height(GAME_IMAGE_HEIGHT));
        }
    }
}
