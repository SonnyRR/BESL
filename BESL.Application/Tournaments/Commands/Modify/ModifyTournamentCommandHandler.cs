namespace BESL.Application.Tournaments.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure.Cloudinary;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    public class ModifyTournamentCommandHandler : IRequestHandler<ModifyTournamentCommand>
    {
        private readonly IDeletableEntityRepository<Tournament> repository;
        private readonly IConfiguration configuration;
        private readonly CloudinaryHelper cloudinaryHelper;

        public ModifyTournamentCommandHandler(IDeletableEntityRepository<Tournament> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
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
                var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

                var url = await this.cloudinaryHelper.UploadImage(
                        cloudinary,
                        request.TournamentImage,
                        name: $"{request.Name}-tournament-main-shot");

                desiredTournament.TournamentImageUrl = url;
            }

            desiredTournament.Name = request.Name;
            desiredTournament.Description = request.Description;
            desiredTournament.StartDate = request.StartDate;
            desiredTournament.EndDate = request.EndDate;

            this.repository.Update(desiredTournament);
            await this.repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
