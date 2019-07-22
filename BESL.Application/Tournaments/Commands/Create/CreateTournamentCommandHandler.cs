namespace BESL.Application.Tournaments.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;    
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure.Cloudinary;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly IConfiguration configuration;
        private readonly CloudinaryHelper cloudinaryHelper;

        public CreateTournamentCommandHandler(
            IDeletableEntityRepository<Tournament> tournamentRepository, 
            IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository,
            IConfiguration configuration)
        {
            this.tournamentRepository = tournamentRepository;
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
        }

        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (this.tournamentRepository.AllAsNoTrackingWithDeleted().Any(t => t.Name == request.Name))
            {
                throw new EntityAlreadyExists(nameof(Tournament), request.Name);
            }

            var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

            var url = await this.cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot");

            var gameId = (await this.tournamentFormatsRepository.GetByIdWithDeletedAsync(request.FormatId))?.GameId;

            if (gameId == null)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.FormatId);
            }

            Tournament tournament = new Tournament()
            {
                Name = request.Name,
                Description = request.Description,
                TournamentImageUrl = url,
                CreatedOn = DateTime.UtcNow,
                FormatId = request.FormatId,
                GameId = (int)gameId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = true
            };

            tournament.Tables.Add(new TournamentTable() { Name = "Open", CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = 50 });
            tournament.Tables.Add(new TournamentTable() { Name = "Mid", CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = 50 });
            tournament.Tables.Add(new TournamentTable() { Name = "Premiership", CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = 20 });

            await this.tournamentRepository.AddAsync(tournament);
            await this.tournamentRepository.SaveChangesAsync(cancellationToken);

            return tournament.Id;
        }
    }
}
