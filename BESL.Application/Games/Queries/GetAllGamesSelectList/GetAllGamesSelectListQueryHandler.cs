namespace BESL.Application.Games.Queries.GetAllGamesSelectList
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;

    public class GetAllGamesSelectListQueryHandler : IRequestHandler<GetAllGamesSelectListQuery, IEnumerable<GameLookupModel>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameLookupModel>> Handle(GetAllGamesSelectListQuery request, CancellationToken cancellationToken)
        {
            var games = await this.dbContext
                .Games
                .ProjectTo<GameLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return games;
        }
    }
}
