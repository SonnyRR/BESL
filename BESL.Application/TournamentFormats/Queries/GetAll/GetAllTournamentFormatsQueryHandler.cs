namespace BESL.Application.TournamentFormats.Queries.GetAll
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
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
                .Where(e => !e.Game.IsDeleted)
                .ProjectTo<TournamentFormatLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var viewModel = new GetAllTournamentFormatsQueryViewModel(tournamentFormatsLookups);
            return viewModel;
        }
    }
}
