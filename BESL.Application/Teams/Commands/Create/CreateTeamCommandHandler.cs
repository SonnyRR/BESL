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
    using BESL.Application.Infrastructure;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<TournamentFormat> formatsRepository;
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public CreateTeamCommandHandler(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<TournamentFormat> formatsRepository,
            IDeletableEntityRepository<PlayerTeam> playerTeamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            ICloudinaryHelper cloudinaryHelper,
            IMapper mapper,
            IUserAccessor userAccessor)
        {
            this.teamsRepository = teamsRepository;
            this.formatsRepository = formatsRepository;
            this.playerTeamsRepository = playerTeamsRepository;
            this.playersRepository = playersRepository;
            this.cloudinaryHelper = cloudinaryHelper;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentUserId = this.userAccessor.UserId;

            if (!await CommonCheckHelper.CheckIfPlayerHasLinkedSteamAccount(currentUserId, this.playersRepository))
            {
                throw new PlayerDoesNotHaveALinkedSteamAccountException();
            }

            if (!await this.CheckIfTournamentFormatExists(request))
            {
                throw new NotFoundException(nameof(TournamentFormat), request.TournamentFormatId);
            }

            if (await CommonCheckHelper.CheckIfPlayerIsAlreadyInATeamWithTheSameFormat(currentUserId, request.TournamentFormatId, this.playerTeamsRepository))
            {
                throw new PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException();
            }

            if (await this.CheckIfTeamWithTheSameNameExists(request))
            {
                throw new EntityAlreadyExistsException(nameof(Team), request.Name);
            }

            var team = this.mapper.Map<Team>(request);
            team.OwnerId = currentUserId;

            if (request.TeamImage != null)
            {
                team.ImageUrl = await this.UploadTeamImage(request);
            }

            team.PlayerTeams.Add(new PlayerTeam() { Team = team, PlayerId = currentUserId });

            await this.teamsRepository.AddAsync(team);
            await this.teamsRepository.SaveChangesAsync(cancellationToken);

            return team.Id;
        }

        private async Task<string> UploadTeamImage(CreateTeamCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                    request.TeamImage,
                    name: $"{request.TeamImage}-team-main-shot",
                    transformation: new Transformation().Width(TEAM_AVATAR_WIDTH).Height(TEAM_AVATAR_HEIGHT));
        }

        private async Task<bool> CheckIfTeamWithTheSameNameExists(CreateTeamCommand request)
        {
            return await this.teamsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Name == request.Name);
        }

        private async Task<bool> CheckIfTournamentFormatExists(CreateTeamCommand request)
        {
            return await this.formatsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(f => f.Id == request.TournamentFormatId);
        }
    }
}