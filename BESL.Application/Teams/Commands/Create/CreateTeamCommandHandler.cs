namespace BESL.Application.Teams.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Infrastructure.Cloudinary;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand>
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> formatRepository;
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamRepository;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMapper mapper;

        public CreateTeamCommandHandler(
            IDeletableEntityRepository<Team> teamRepository,
            IDeletableEntityRepository<TournamentFormat> formatRepository,
            IDeletableEntityRepository<PlayerTeam> playerRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.formatRepository = formatRepository;
            this.playerTeamRepository = playerRepository;
            this.configuration = configuration;
            this.cloudinaryHelper = new CloudinaryHelper();
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var isPlayerAlreadyInATeamWithTheSameFormat = this.playerTeamRepository
                .AllAsNoTrackingWithDeleted()
                .Include(pt => pt.Team)
                .Where(pt => pt.PlayerId == request.OwnerId)
                .Any(pt => pt.Team.TournamentFormatId == request.TournamentFormatId);
            
            if (isPlayerAlreadyInATeamWithTheSameFormat)
            {
                throw new PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException();
            }

            var doesTeamAlreadyExist = this.teamRepository
                .AllAsNoTrackingWithDeleted()
                .Any(t => t.Name == request.Name);

            if (doesTeamAlreadyExist)
            {
                throw new EntityAlreadyExists(nameof(Team), request.Name);
            }

            var doesTournamentFormatExist = this.formatRepository
                .AllAsNoTrackingWithDeleted()
                .Any(f => f.Id == request.TournamentFormatId);
            
            if (!doesTournamentFormatExist)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.TournamentFormatId);
            }

            var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

            var url = await this.cloudinaryHelper.UploadImage(
                    cloudinary,
                    request.TeamImage,
                    name: $"{request.TeamImage}-team-main-shot",
                    transformation: new Transformation().Width(TEAM_AVATAR_WIDTH).Height(TEAM_AVATAR_HEIGHT));

            var team = this.mapper.Map<Team>(request);
            team.ImageUrl = url;

            await this.teamRepository.AddAsync(team);
            await this.playerTeamRepository.AddAsync(new PlayerTeam() { Team = team, PlayerId = request.OwnerId });
            await this.teamRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}