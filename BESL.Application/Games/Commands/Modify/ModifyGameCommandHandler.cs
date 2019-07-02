namespace BESL.Application.Games.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using BESL.Common;

    public class ModifyGameCommandHandler : IRequestHandler<ModifyGameCommand, Unit>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public ModifyGameCommandHandler(IApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<Unit> Handle(ModifyGameCommand request, CancellationToken cancellationToken)
        {
            var existingGame = await this.dbContext
                .Games
                .SingleOrDefaultAsync(g => g.Id == request.Id);

            if (existingGame == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            existingGame.Name = request.Name;
            existingGame.Description = request.Description;

            if (request.GameImage != null)
            {
                var cloudinary = CloudinaryHelper.GetInstance(this.configuration);
                var imageUrl = await CloudinaryHelper.UploadImage(
                    cloudinary,
                    request.GameImage,
                    name: $"{request.Name}-main-shot"
                //transformation: new Transformation().Width(500).Height(500)
                );
                existingGame.GameImageUrl = imageUrl;
            }

            existingGame.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
