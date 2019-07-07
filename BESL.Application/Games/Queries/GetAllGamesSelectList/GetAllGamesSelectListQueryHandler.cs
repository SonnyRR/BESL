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
    using BESL.Application.Common.Models;

    public class GetAllGamesSelectListQueryHandler : IRequestHandler<GetAllGamesSelectListQuery, IEnumerable<GameSelectItemLookupModel>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameSelectItemLookupModel>> Handle(GetAllGamesSelectListQuery request, CancellationToken cancellationToken)
        {
            var games = await this.dbContext
                .Games
                .ProjectTo<GameSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return games;
        }
    }
}
