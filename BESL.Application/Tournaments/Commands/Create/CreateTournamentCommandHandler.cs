﻿namespace BESL.Application.Tournaments.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Common.Cloudinary;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> repository;
        private readonly IConfiguration configuration;
        private readonly CloudinaryHelper cloudinaryHelper;

        public CreateTournamentCommandHandler(IDeletableEntityRepository<Tournament> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
        }

        public async Task<int> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (this.repository.AllAsNoTrackingWithDeleted().Any(t => t.Name == request.Name))
            {
                throw new EntityAlreadyExists(nameof(Tournament), request.Name);
            }

            var cloudinary = cloudinaryHelper.GetInstance(this.configuration);

            var url = await cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.TournamentImage,
                    name: $"{request.Name}-tournament-main-shot"
                );

            var gameId = (await this.repository.GetByIdWithDeletedAsync(request.FormatId)).GameId;

            Tournament tournament = new Tournament()
            {
                Name = request.Name,
                Description = request.Description,
                TournamentImageUrl = url,
                CreatedOn = DateTime.UtcNow,
                FormatId = request.FormatId,
                GameId = gameId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            tournament.Tables.Add(new TournamentTable() { Name = "Open", CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = 50 });
            tournament.Tables.Add(new TournamentTable() { Name = "Mid", CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = 50 });
            tournament.Tables.Add(new TournamentTable() { Name = "Premiership", CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = 20 });

            await this.repository.AddAsync(tournament);
            await this.repository.SaveChangesAsync(cancellationToken);

            return tournament.Id;
        }
    }
}
