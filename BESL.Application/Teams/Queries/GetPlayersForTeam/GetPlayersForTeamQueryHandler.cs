namespace BESL.Application.Teams.Queries.GetPlayersForTeam
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

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
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;

        public GetPlayersForTeamQueryHandler(IMapper mapper, IDeletableEntityRepository<PlayerTeam> playerTeamsRepository)
        {
            this.mapper = mapper;
            this.playerTeamsRepository = playerTeamsRepository;
        }

        public async Task<GetPlayersForTeamViewModel> Handle(GetPlayersForTeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var teamPlayers = await this.playerTeamsRepository
                .AllAsNoTracking()               
                .Where(pt => pt.TeamId == request.TeamId)
                    .Include(pt => pt.Player)
                    .Include(pt => pt.Team)
                .Select(x => x.Player)
                .ProjectTo<PlayerLookup>(this.mapper.ConfigurationProvider, new Dictionary<string, object>(1) { { "teamId", request.TeamId } })
                .OrderByDescending(p => p.isOwner)
                .ToListAsync(cancellationToken);

            var viewModel = new GetPlayersForTeamViewModel { Players = teamPlayers };
            return viewModel;
        }
    }
}
