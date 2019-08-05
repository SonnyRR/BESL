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
    using BESL.Application.TournamentTables.Commands.Create;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMediator mediator;

        public CreateTournamentCommandHandler(
            IDeletableEntityRepository<Tournament> tournamentsRepository, 
            IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository,
            ICloudinaryHelper cloudinaryHelper,
            IMediator mediator)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
            this.mediator = mediator;
        }

        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await this.CheckIfTournamentWithGivenNameAlreadyExists(request))
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

            await this.tournamentsRepository.AddAsync(tournament);
            await this.tournamentsRepository.SaveChangesAsync(cancellationToken);

            await this.mediator.Send(new CreateTablesForTournamentCommand { TournamentId = tournament.Id });

            return tournament.Id;
        }

        private async Task<string> UploadImage(CreateTournamentCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot",
                    transformation: new Transformation().Width(TOURNAMENT_IMAGE_WIDTH).Height(TOURNAMENT_IMAGE_HEIGHT));
        }

        private async Task<bool> CheckIfTournamentWithGivenNameAlreadyExists(CreateTournamentCommand request)
        {
            return await this.tournamentsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Name == request.Name);
        }
    }
}
