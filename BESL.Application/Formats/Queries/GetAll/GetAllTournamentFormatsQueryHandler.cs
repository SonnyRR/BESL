namespace BESL.Application.Formats.Queries.GetAll
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class GetAllTournamentFormatsQueryHandler : IRequestHandler<GetAllTournamentFormatsQuery, GetAllTournamentFormatsQueryViewModel>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllTournamentFormatsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GetAllTournamentFormatsQueryViewModel> Handle(GetAllTournamentFormatsQuery request, CancellationToken cancellationToken)
        {
            var tournamentFormatsLookups = await this.dbContext
                .TournamentFormats
                .ProjectTo<TournamentFormatLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var viewModel = new GetAllTournamentFormatsQueryViewModel(tournamentFormatsLookups);
            return viewModel;
        }
    }
}
