namespace BESL.Application.TournamentFormats.Queries.GetAll
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using BESL.Domain.Entities;

    public class GetAllTournamentFormatsQueryHandler : IRequestHandler<GetAllTournamentFormatsQuery, GetAllTournamentFormatsQueryViewModel>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> repository;
        private readonly IMapper mapper;

        public GetAllTournamentFormatsQueryHandler(IDeletableEntityRepository<TournamentFormat> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GetAllTournamentFormatsQueryViewModel> Handle(GetAllTournamentFormatsQuery request, CancellationToken cancellationToken)
        {
            var tournamentFormatsLookups = await this.repository
                .AllAsNoTracking()
                .ProjectTo<TournamentFormatLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var viewModel = new GetAllTournamentFormatsQueryViewModel(tournamentFormatsLookups);
            return viewModel;
        }
    }
}
