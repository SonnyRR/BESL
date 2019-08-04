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

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> formatRepository;
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMapper mapper;

        public CreateTeamCommandHandler(
            IDeletableEntityRepository<Team> teamRepository,
            IDeletableEntityRepository<TournamentFormat> formatRepository,
            IDeletableEntityRepository<PlayerTeam> playerRepository,
            ICloudinaryHelper cloudinaryHelper,
            IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.formatRepository = formatRepository;
            this.playerTeamRepository = playerRepository;
            this.cloudinaryHelper = cloudinaryHelper;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var isPlayerAlreadyInATeamWithTheSameFormat = await this.CheckIfOwnerIsAlreadyInATeamWithTheSameFormat(request);
            if (isPlayerAlreadyInATeamWithTheSameFormat)
            {
                throw new PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException();
            }

            var doesTeamAlreadyExist = await this.CheckIfTeamWithTheSameNameExists(request);
            if (doesTeamAlreadyExist)
            {
                throw new EntityAlreadyExistsException(nameof(Team), request.Name);
            }

            var doesTournamentFormatExist = await this.CheckIfTournamentFormatExists(request);
            if (!doesTournamentFormatExist)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.TournamentFormatId);
            }

            var url = await this.cloudinaryHelper.UploadImage(
                    request.TeamImage,
                    name: $"{request.TeamImage}-team-main-shot",
                    transformation: new Transformation().Width(TEAM_AVATAR_WIDTH).Height(TEAM_AVATAR_HEIGHT));

            var team = this.mapper.Map<Team>(request);
            team.ImageUrl = url;

            await this.teamRepository.AddAsync(team);
            await this.playerTeamRepository.AddAsync(new PlayerTeam() { Team = team, PlayerId = request.OwnerId });
            await this.teamRepository.SaveChangesAsync(cancellationToken);

            return team.Id;
        }

        private async Task<bool> CheckIfOwnerIsAlreadyInATeamWithTheSameFormat(CreateTeamCommand request)
        {
            return await this.playerTeamRepository
                   .AllAsNoTrackingWithDeleted()
                   .Include(pt => pt.Team)
                   .Where(pt => pt.PlayerId == request.OwnerId)
                   .AnyAsync(pt => pt.Team.TournamentFormatId == request.TournamentFormatId);
        }

        private async Task<bool> CheckIfTeamWithTheSameNameExists(CreateTeamCommand request)
        {
            return await this.teamRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Name == request.Name);
        }

        private async Task<bool> CheckIfTournamentFormatExists(CreateTeamCommand request)
        {
            return await this.formatRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(f => f.Id == request.TournamentFormatId);
        }
    }
}