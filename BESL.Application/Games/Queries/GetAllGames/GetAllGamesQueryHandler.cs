namespace BESL.Application.Games.Queries.GetAllGames
{    
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, GamesListViewModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllGamesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GamesListViewModel> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            GamesListViewModel viewModel = new GamesListViewModel();

            viewModel.Games = await this.context
                .Games
                .ProjectTo<GameLookupModel>(this.mapper.ConfigurationProvider) // TODO
                .ToListAsync(cancellationToken);

            return viewModel;
        }
    }
}
