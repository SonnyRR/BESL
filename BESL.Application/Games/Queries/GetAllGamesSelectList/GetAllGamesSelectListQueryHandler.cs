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

    public class GetAllGamesSelectListQueryHandler : IRequestHandler<GetAllGamesSelectListQuery, IEnumerable<GameSelectItemLookupModel>>
    {
        private readonly IDeletableEntityRepository<Game> repository;
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryHandler(IDeletableEntityRepository<Game> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameSelectItemLookupModel>> Handle(GetAllGamesSelectListQuery request, CancellationToken cancellationToken)
        {
            var games = await this.repository
                .AllAsNoTracking()
                .ProjectTo<GameSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return games;
        }
    }
}
