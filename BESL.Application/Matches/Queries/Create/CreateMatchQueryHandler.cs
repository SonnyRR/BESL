namespace BESL.Application.Matches.Queries.Create
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
    using BESL.Application.Matches.Commands.Create;
    using BESL.Domain.Entities;

    public class CreateMatchQueryHandler : IRequestHandler<CreateMatchQuery, CreateMatchCommand>
    {
        private readonly IDeletableEntityRepository<TournamentTable> tournamentTablesRepository;
        private readonly IMapper mapper;

        public CreateMatchQueryHandler(IDeletableEntityRepository<TournamentTable> tournamentTablesRepository, IMapper mapper)
        {
            this.tournamentTablesRepository = tournamentTablesRepository;
            this.mapper = mapper;
        }

        public async Task<CreateMatchCommand> Handle(CreateMatchQuery request, CancellationToken cancellationToken)
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

            var viewModel = new CreateMatchCommand { Teams = teams, TournamentTableId = request.TournamentTableId, PlayWeekId = request.PlayWeekId };
            return viewModel;
        }
    }
}
