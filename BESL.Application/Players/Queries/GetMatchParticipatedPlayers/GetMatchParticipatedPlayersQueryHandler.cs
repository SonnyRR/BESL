namespace BESL.Application.Players.Queries.GetMatchParticipatedPlayers
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

    public class GetMatchParticipatedPlayersQueryHandler : IRequestHandler<GetMatchParticipatedPlayersQuery, MatchParticipatedPlayersViewModel>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public GetMatchParticipatedPlayersQueryHandler(IDeletableEntityRepository<Match> matchesRepository, IMapper mapper)
        {
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
        }

        public async Task<MatchParticipatedPlayersViewModel> Handle(GetMatchParticipatedPlayersQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var participatedPlayersLookups = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.ParticipatedPlayers)
                    .ThenInclude(pp => pp.Player)
                .Where(m => m.Id == request.MatchId)
                .SelectMany(m => m.ParticipatedPlayers.Select(pm => pm.Player))
                .OrderBy(x => x.UserName)
                .ProjectTo<PlayerLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new MatchParticipatedPlayersViewModel { Players = participatedPlayersLookups }; 
            return viewModel;
        }
    }
}
