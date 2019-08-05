namespace BESL.Application.Tournaments.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;    
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;
    using static BESL.Common.GlobalConstants;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public CreateTournamentCommandHandler(
            IDeletableEntityRepository<Tournament> tournamentRepository, 
            IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository,
            ICloudinaryHelper cloudinaryHelper)
        {
            this.tournamentRepository = tournamentRepository;
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (this.tournamentRepository.AllAsNoTrackingWithDeleted().Any(t => t.Name == request.Name))
            {
                throw new EntityAlreadyExistsException(nameof(Tournament), request.Name);
            }

            var gameId = (await this.tournamentFormatsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(tf => tf.Id == request.FormatId, cancellationToken))
                ?.GameId
                ?? throw new NotFoundException(nameof(TournamentFormat), request.FormatId);

            Tournament tournament = new Tournament()
            {
                Name = request.Name,
                Description = request.Description,
                TournamentImageUrl = await this.UploadImage(request),
                CreatedOn = DateTime.UtcNow,
                FormatId = request.FormatId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = true,
                AreSignupsOpen = true
            };

            tournament.Tables.Add(new TournamentTable() { Name = OPEN_TABLE_NAME, CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = OPEN_TABLE_MAX_TEAMS });
            tournament.Tables.Add(new TournamentTable() { Name = MID_TABLE_NAME, CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = MID_TABLE_MAX_TEAMS });
            tournament.Tables.Add(new TournamentTable() { Name = PREM_TABLE_NAME, CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = PREM_TABLE_MAX_TEAMS });

            await this.tournamentRepository.AddAsync(tournament);
            await this.tournamentRepository.SaveChangesAsync(cancellationToken);

            return tournament.Id;
        }

        private async Task<string> UploadImage(CreateTournamentCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot",
                    transformation: new Transformation().Width(TOURNAMENT_IMAGE_WIDTH).Height(TOURNAMENT_IMAGE_HEIGHT));
        }
    }
}
