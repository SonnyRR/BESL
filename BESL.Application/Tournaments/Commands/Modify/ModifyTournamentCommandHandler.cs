namespace BESL.Application.Tournaments.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Entities;
    using static BESL.Common.GlobalConstants;

    public class ModifyTournamentCommandHandler : IRequestHandler<ModifyTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyTournamentCommandHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, ICloudinaryHelper cloudinaryHelper)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(ModifyTournamentCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.Id);

            if (request.Name != desiredTournament.Name && await this.CheckIfTournamentWithTheSameNameExists(request.Name))
            {
                throw new EntityAlreadyExistsException(nameof(Tournament), request.Name);
            }

            if (request.TournamentImage != null)
            {
                desiredTournament.TournamentImageUrl = await this.UploadImage(request);
            }

            desiredTournament.Name = request.Name;
            desiredTournament.Description = request.Description;
            desiredTournament.StartDate = request.StartDate;
            desiredTournament.EndDate = request.EndDate;
            desiredTournament.IsActive = request.IsActive;
            desiredTournament.AreSignupsOpen = request.AreSignupsOpen;

            this.tournamentsRepository.Update(desiredTournament);

            await this.tournamentsRepository.SaveChangesAsync(cancellationToken);
            return desiredTournament.Id;
        }

        private async Task<string> UploadImage(ModifyTournamentCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot",
                    transformation: new Transformation().Width(TOURNAMENT_IMAGE_WIDTH).Height(TOURNAMENT_IMAGE_HEIGHT));
        }

        private async Task<bool> CheckIfTournamentWithTheSameNameExists(string name)
        {
            return await this.tournamentsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Name == name);
        }
    }
}
