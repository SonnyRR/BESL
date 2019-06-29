namespace BESL.Application.Formats.Queries.GetAllGames
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class GetAllGamesCommandHandler : IRequestHandler<GetAllGamesCommand, GetAllGamesListViewModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllGamesCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetAllGamesListViewModel> Handle(GetAllGamesCommand request, CancellationToken cancellationToken)
        {
            var gamesDtoList = await this.context
                .Games
                .ProjectTo<GameLookupModel>()
                .ToListAsync(cancellationToken);

            var viewModel = new GetAllGamesListViewModel()
            {
                Games = gamesDtoList
            };

            return viewModel;
        }
    }
}
