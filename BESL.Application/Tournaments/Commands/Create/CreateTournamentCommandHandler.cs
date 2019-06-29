namespace BESL.Application.Tournaments.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Common;
    using BESL.Domain.Entities;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IConfiguration configuration;

        public CreateTournamentCommandHandler(IApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            if (this.context.Tournaments.Any(t => t.Name == request.Name))
            {
                // Throw
            }

            var cloudinary = CloudinaryHelper.GetInstance(this.configuration);

            var url = await CloudinaryHelper.UploadImage(
                    cloudinary,
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot"
                //transformation: new Transformation().Width(500).Height(500)
                );

            Tournament tournament = new Tournament()
            {
                Name = request.Name,
                Description = request.Description,
                TournamentImageUrl = url
            };

            this.context.Tournaments.Add(tournament);
            await this.context.SaveChangesAsync(cancellationToken);

            return tournament.Id;
        }
    }
}
