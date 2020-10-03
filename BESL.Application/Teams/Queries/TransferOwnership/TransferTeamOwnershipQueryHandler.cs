namespace BESL.Application.Teams.Queries.TransferOwnership
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.TransferOwnership;
    using BESL.Entities;

    public class TransferTeamOwnershipQueryHandler : IRequestHandler<TransferTeamOwnershipQuery, TransferTeamOwnershipCommand>
    {
        private readonly IDeletableEntityRepository<PlayerTeam> playerTeamsRepository;
        private readonly IMapper mapper;

        public TransferTeamOwnershipQueryHandler(IDeletableEntityRepository<PlayerTeam> playerTeamsRepository, IMapper mapper)
        {
            this.playerTeamsRepository = playerTeamsRepository;
            this.mapper = mapper;
        }

        public async Task<TransferTeamOwnershipCommand> Handle(TransferTeamOwnershipQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var teamPlayers = await this.playerTeamsRepository
                .AllAsNoTracking()
                .Where(pt => pt.TeamId == request.TeamId)
                .Select(x => x.Player)
                .ProjectTo<PlayerSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var command = new TransferTeamOwnershipCommand { TeamPlayers = teamPlayers, TeamId = request.TeamId };
            return command;
        }
    }
}
