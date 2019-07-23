namespace BESL.Application.Games.Queries.GetAllGames
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, GamesListViewModel>
    {
        private readonly IDeletableEntityRepository<Game> repository;
        private readonly IMapper mapper;

        public GetAllGamesQueryHandler(IDeletableEntityRepository<Game> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GamesListViewModel> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            GamesListViewModel viewModel = new GamesListViewModel();

            viewModel.Games = await this.repository
                .AllAsNoTracking()
                    .Include(g => g.TournamentFormats)
                    .ThenInclude(tf => tf.Tournaments)
                .ProjectTo<GameLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return viewModel;
        }
    }
}
