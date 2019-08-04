namespace BESL.Application.Teams.Queries.GetPlayersForTeam
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    public class GetPlayersForTeamQueryHandler : IRequestHandler<GetPlayersForTeamQuery, GetPlayersForTeamViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Player> playerRepository;
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public GetPlayersForTeamQueryHandler(
            IMapper mapper,
            IDeletableEntityRepository<Player> playerRepository,
            IDeletableEntityRepository<Team> teamRepository)
        {
            this.mapper = mapper;
            this.playerRepository = playerRepository;
            this.teamRepository = teamRepository;
        }

        public async Task<GetPlayersForTeamViewModel> Handle(GetPlayersForTeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var doesTeamExist = await this.DoesTeamExist(request);

            if (!doesTeamExist)
            {
                throw new NotFoundException(nameof(Team), request.TeamId);
            }

            var teamPlayers = await this.teamRepository
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
            return await this.teamRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == request.TeamId);
        }
    }
}
