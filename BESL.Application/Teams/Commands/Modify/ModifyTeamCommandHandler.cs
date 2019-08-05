namespace BESL.Application.Teams.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;
    using static BESL.Common.GlobalConstants;

    public class ModifyTeamCommandHandler : IRequestHandler<ModifyTeamCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public ModifyTeamCommandHandler(IDeletableEntityRepository<Team> teamsRepository, ICloudinaryHelper cloudinaryHelper)
        {
            this.teamsRepository = teamsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(ModifyTeamCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.Id);

            if (request.Name != desiredTeam.Name)
            {
                if (await this.CheckIfTeamWithGivenNameAlreadyExists(request.Name))
                {
                    throw new EntityAlreadyExistsException(nameof(Team), request.Name);
                }

                desiredTeam.Name = request.Name;
            }

            desiredTeam.Description = request.Description;
            desiredTeam.HomepageUrl = request.HomepageUrl;

            if (request.TeamImage != null)
            {
                desiredTeam.ImageUrl = await this.UploadImage(request);
            }


            this.teamsRepository.Update(desiredTeam);
            return await this.teamsRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task<string> UploadImage(ModifyTeamCommand request)
        {
            var url = await this.cloudinaryHelper.UploadImage(
                    request.TeamImage,
                    name: $"{request.TeamImage}-team-main-shot",
                    transformation: new Transformation().Width(TEAM_AVATAR_WIDTH).Height(TEAM_AVATAR_HEIGHT));
            return url;
        }

        private async Task<bool> CheckIfTeamWithGivenNameAlreadyExists(string name)
        {
            return await this.teamsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(t => t.Name == name);
        }
    }
}
