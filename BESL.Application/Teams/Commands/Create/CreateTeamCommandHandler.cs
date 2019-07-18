namespace BESL.Application.Teams.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using static BESL.Common.GlobalConstants;
    using BESL.Domain.Entities;
    using BESL.Application.Infrastructure.Cloudinary;
    using Microsoft.Extensions.Configuration;
    using System;
    using CloudinaryDotNet;
    using AutoMapper;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand>
    {
        private readonly IDeletableEntityRepository<Team> teams;
        private readonly IConfiguration configuration;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly IMapper mapper;

        public CreateTeamCommandHandler(
            IDeletableEntityRepository<Team> teams,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.teams = teams;
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

            var a = this.mapper.Map<Team>(request);

            return Unit.Value;
            //var cloudinary = this.cloudinaryHelper.GetInstance(this.configuration);

            //var url = await this.cloudinaryHelper.UploadImage(
            //        cloudinary,
            //        request.TeamImage,
            //        name: $"{request.TeamImage}-team-main-shot",
            //        transformation: new Transformation().Width(TEAM_AVATAR_WIDTH).Height(TEAM_AVATAR_HEIGHT)
            //    );            
        }
    }
}
