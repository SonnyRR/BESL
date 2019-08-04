namespace BESL.Application.Tournaments.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class ModifyTournamentCommandHandler : IRequestHandler<ModifyTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyTournamentCommandHandler(IDeletableEntityRepository<Tournament> tournamentRepository, ICloudinaryHelper cloudinaryHelper)
        {
            this.tournamentRepository = tournamentRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(ModifyTournamentCommand request, CancellationToken cancellationToken)
        {

            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTournament = await this.tournamentRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.Id);

            if (request.TournamentImage != null)
            {
                desiredTournament.TournamentImageUrl = await this.UploadImage(request);
            }

            desiredTournament.Name = request.Name;
            desiredTournament.Description = request.Description;
            desiredTournament.StartDate = request.StartDate;
            desiredTournament.EndDate = request.EndDate;
            desiredTournament.IsActive = request.IsActive;

            this.tournamentRepository.Update(desiredTournament);
            return await this.tournamentRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task<string> UploadImage(ModifyTournamentCommand request)
        {
            var url = await this.cloudinaryHelper.UploadImage(
                       request.TournamentImage,
                       name: $"{request.Name}-tournament-main-shot");
            return url;
        }

        private async Task<bool> CheckIfTournamentWithTheSameNameExists(string name)
        {
            return await this.tournamentRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Name == name);
        }
    }
}
