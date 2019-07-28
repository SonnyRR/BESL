namespace BESL.Application.Tournaments.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    public class ModifyTournamentCommandHandler : IRequestHandler<ModifyTournamentCommand>
    {
        private readonly IDeletableEntityRepository<Tournament> repository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyTournamentCommandHandler(IDeletableEntityRepository<Tournament> repository, ICloudinaryHelper cloudinaryHelper)
        {
            this.repository = repository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<Unit> Handle(ModifyTournamentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredTournament = await this.repository.GetByIdWithDeletedAsync(request.Id);

            if (desiredTournament == null)
            {
                throw new NotFoundException(nameof(Tournament), request.Id);
            }

            if (request.TournamentImage != null)
            {                
                var url = await this.cloudinaryHelper.UploadImage(
                        request.TournamentImage,
                        name: $"{request.Name}-tournament-main-shot");

                desiredTournament.TournamentImageUrl = url;
            }

            desiredTournament.Name = request.Name;
            desiredTournament.Description = request.Description;
            desiredTournament.StartDate = request.StartDate;
            desiredTournament.EndDate = request.EndDate;
            desiredTournament.IsActive = request.IsActive;

            this.repository.Update(desiredTournament);
            await this.repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
