namespace BESL.Application.Teams.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using static BESL.Common.GlobalConstants;
    using BESL.Domain.Entities;
    using BESL.Application.Infrastructure.Cloudinary;
    using BESL.Application.Exceptions;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand>
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> formatRepository;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMapper mapper;

        public CreateTeamCommandHandler(
            IDeletableEntityRepository<Team> teamRepository,
            IDeletableEntityRepository<TournamentFormat> formatRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.formatRepository = formatRepository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //var gameId = (await this.formatRepository.GetByIdWithDeletedAsync(request.TournamentFormatId))?.GameId;
            //if (gameId == null)
            //{
            //    throw new NotFoundException(nameof(TournamentFormat), request.TournamentFormatId);
            //}

            var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

            var url = await this.cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.TeamImage,
                    name: $"{request.TeamImage}-team-main-shot",
                    transformation: new Transformation().Width(TEAM_AVATAR_WIDTH).Height(TEAM_AVATAR_HEIGHT)
                );

            var team = this.mapper.Map<Team>(request);
            team.ImageUrl = url;

            await this.teamRepository.AddAsync(team);
            await this.teamRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
