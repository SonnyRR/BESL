namespace BESL.Application.Teams.Queries.GetPlayersForTeam
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class GetPlayersForTeamQueryHandler : IRequestHandler<GetPlayersForTeamQuery, GetPlayersForTeamViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public GetPlayersForTeamQueryHandler(IMapper mapper, IDeletableEntityRepository<Team> teamRepository)
        {
            this.mapper = mapper;
            this.teamsRepository = teamRepository;
        }

        public async Task<GetPlayersForTeamViewModel> Handle(GetPlayersForTeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.DoesTeamExist(request))
            {
                throw new NotFoundException(nameof(Team), request.TeamId);
            }

            var teamPlayers = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.PlayerTeams)
                    .ThenInclude(pt => pt.Player)
                    .ThenInclude(p => p.Claims)
                .Where(t => t.Id == request.TeamId)
                .SelectMany(x => x.PlayerTeams.Select(y => y.Player))
                .ProjectTo<PlayerLookup>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new GetPlayersForTeamViewModel { Players = teamPlayers };
            return viewModel;
        }

        private async Task<bool> DoesTeamExist(GetPlayersForTeamQuery request)
        {
            return await this.teamsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == request.TeamId);
        }
    }
}
