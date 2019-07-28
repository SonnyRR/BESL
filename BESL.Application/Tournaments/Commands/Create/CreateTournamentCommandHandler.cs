﻿namespace BESL.Application.Tournaments.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using CloudinaryDotNet;
    using MediatR;

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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (this.tournamentRepository.AllAsNoTrackingWithDeleted().Any(t => t.Name == request.Name))
            {
                throw new EntityAlreadyExists(nameof(Tournament), request.Name);
            }

            var url = await this.cloudinaryHelper.UploadImage(
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot",
                    transformation: new Transformation().Width(TOURNAMENT_IMAGE_WIDTH).Height(TOURNAMENT_IMAGE_HEIGHT));

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
