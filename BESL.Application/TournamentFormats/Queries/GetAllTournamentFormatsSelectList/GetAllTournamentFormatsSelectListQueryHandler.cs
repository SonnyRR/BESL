namespace BESL.Application.TournamentFormats.Queries.GetAllTournamentFormatsSelectList
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Common.Models.Lookups;
    using BESL.Entities;

    public class GetAllTournamentFormatsSelectListQueryHandler 
        : IRequestHandler<GetAllTournamentFormatsSelectListQuery, IEnumerable<TournamentFormatSelectItemLookupModel>>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly IMapper mapper;

        public GetAllTournamentFormatsSelectListQueryHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository, IMapper mapper)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TournamentFormatSelectItemLookupModel>> Handle(
            GetAllTournamentFormatsSelectListQuery request, 
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var lookups = await this.tournamentFormatsRepository
                .AllAsNoTracking()
                .ProjectTo<TournamentFormatSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return lookups;
        }
    }
}
