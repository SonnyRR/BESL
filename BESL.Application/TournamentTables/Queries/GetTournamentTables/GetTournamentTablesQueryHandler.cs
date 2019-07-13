namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;
    using AutoMapper.QueryableExtensions;
    using BESL.Domain.Entities;

    public class GetTournamentTablesQueryHandler : IRequestHandler<GetTournamentTablesQuery, TournamentTablesViewModel>
    {
        private readonly IDeletableEntityRepository<TournamentTable> repository;
        private readonly IMapper mapper;

        public GetTournamentTablesQueryHandler(IDeletableEntityRepository<TournamentTable> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<TournamentTablesViewModel> Handle(GetTournamentTablesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var tables = await this.repository
                .AllAsNoTracking()
                    .Include(tt => tt.SignedUpTeams)
                    .Include(tt => tt.TeamTableResults)
                        .ThenInclude(ttr => ttr.Team)
                .Where(tt => tt.TournamentId == request.TournamentId)
                .ProjectTo<TournamentTableLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var viewModel = new TournamentTablesViewModel() { Tables = tables };
            return viewModel;
        }
    }
}
