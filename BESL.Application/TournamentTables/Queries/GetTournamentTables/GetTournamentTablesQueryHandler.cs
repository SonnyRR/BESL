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

    public class GetTournamentTablesQueryHandler : IRequestHandler<GetTournamentTablesQuery, TournamentTablesViewModel>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetTournamentTablesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TournamentTablesViewModel> Handle(GetTournamentTablesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var tables = await this.dbContext
                .TournamentTables
                    .Include(tt => tt.SignedUpTeams)
                    .Include(tt => tt.TeamTableResults)
                        .ThenInclude(ttr => ttr.Team)
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
