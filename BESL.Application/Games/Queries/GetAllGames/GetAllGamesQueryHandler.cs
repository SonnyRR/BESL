namespace BESL.Application.Games.Queries.GetAllGames
{    
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using System.Linq;

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
                .Where(g => !g.IsDeleted)
                .ProjectTo<GameLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return viewModel;
        }
    }
}
