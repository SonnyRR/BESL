namespace BESL.Application.TournamentTables.Queries.GetTeamsForTournamentTable
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetTeamsForTournamentTableQueryHandler : IRequestHandler<GetTeamsForTournamentTableQuery, TeamsForTournamentTableViewModel>
    {
        private readonly IDeletableEntityRepository<TournamentTable> tournamentTablesRepository;
        private readonly IMapper mapper;

        public GetTeamsForTournamentTableQueryHandler(IDeletableEntityRepository<TournamentTable> tournamentTablesRepository, IMapper mapper)
        {
            this.tournamentTablesRepository = tournamentTablesRepository;
            this.mapper = mapper;
        }

        public async Task<TeamsForTournamentTableViewModel> Handle(GetTeamsForTournamentTableQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var teams = await this.tournamentTablesRepository
                .AllAsNoTracking()
                .Include(t => t.TeamTableResults)
                    .ThenInclude(t => t.Team)
                .Where(t => t.Id == request.TournamentTableId)
                .SelectMany(t => t.TeamTableResults.Select(ttr => ttr.Team))
                .ProjectTo<TeamsSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new TeamsForTournamentTableViewModel { Players = teams };
            return viewModel;
        }
    }
}
