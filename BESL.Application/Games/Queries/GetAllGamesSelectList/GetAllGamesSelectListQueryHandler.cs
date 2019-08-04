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
    using BESL.Domain.Entities;
    using BESL.Application.Common.Models.Lookups;

    public class GetAllGamesSelectListQueryHandler : IRequestHandler<GetAllGamesSelectListQuery, IEnumerable<GameSelectItemLookupModel>>
    {
        private readonly IDeletableEntityRepository<Game> gameRepository;
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryHandler(IDeletableEntityRepository<Game> gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameSelectItemLookupModel>> Handle(GetAllGamesSelectListQuery request, CancellationToken cancellationToken)
        {
            var games = await this.gameRepository
                .AllAsNoTracking()
                .ProjectTo<GameSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return games;
        }
    }
}
