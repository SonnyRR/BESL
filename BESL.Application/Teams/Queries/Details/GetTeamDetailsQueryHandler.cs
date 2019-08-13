namespace BESL.Application.Teams.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetTeamDetailsQueryHandler : IRequestHandler<GetTeamDetailsQuery, GetTeamDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public GetTeamDetailsQueryHandler(IDeletableEntityRepository<Team> teamsRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            this.teamsRepository = teamsRepository;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<GetTeamDetailsViewModel> Handle(GetTeamDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                    .ThenInclude(tf => tf.Game)
                .Include(t => t.PlayerTeams)
                    .ThenInclude(pt => pt.Player)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.Id);

            var lookupModel = this.mapper.Map<TeamDetailsLookupModel>(desiredTeam, opts => opts.Items["CurrentUserId"] = this.userAccessor.UserId);
            var viewModel = this.mapper.Map<GetTeamDetailsViewModel>(lookupModel);

            return viewModel;
        }
    }
}
