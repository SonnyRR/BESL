namespace BESL.Application.Tournaments.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Common;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly CloudinaryHelper cloudinaryHelper;

        public CreateTournamentCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
        }

        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (this.context.Tournaments.Any(t => t.Name == request.Name))
            {
                throw new EntityAlreadyExists(nameof(Tournament), request.Name);
            }

            var cloudinary = cloudinaryHelper.GetInstance(this.configuration);

            var url = await cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot"
                );

            var gameId = (await this.context.TournamentFormats.SingleOrDefaultAsync(tf => tf.Id == request.FormatId)).GameId;
            Tournament tournament = new Tournament()
            {
                Name = request.Name,
                Description = request.Description,
                TournamentImageUrl = url,
                CreatedOn = DateTime.UtcNow,
                FormatId = request.FormatId,
                GameId = gameId,
            };

            this.context.Tournaments.Add(tournament);
            await this.context.SaveChangesAsync(cancellationToken);

            return tournament.Id;
        }
    }
}
