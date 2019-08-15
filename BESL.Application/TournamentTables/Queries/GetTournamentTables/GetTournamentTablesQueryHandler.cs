namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

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
                .Include(tt => tt.TeamTableResults)
                    .ThenInclude(ttr => ttr.Team)
                .Include(tt => tt.PlayWeeks)
                .Where(tt => tt.TournamentId == request.Id)
                .ProjectTo<TournamentTableLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            if (tables.Count == 0)
            {
                throw new NotFoundException(nameof(Tournament), request.Id);
            }

            var viewModel = new TournamentTablesViewModel() { Tables = tables, TournamentId = request.Id };
            return viewModel;
        }
    }
}
