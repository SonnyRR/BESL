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
        private readonly IDeletableEntityRepository<PlayWeek> playweeksRepository;
        private readonly IMapper mapper;

        public CreateMatchQueryHandler(IDeletableEntityRepository<PlayWeek> playweeksRepository, IMapper mapper)
        {
            this.playweeksRepository = playweeksRepository;
            this.mapper = mapper;
        }

        public async Task<CreateMatchCommand> Handle(CreateMatchQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var teams = await this.playweeksRepository
                .AllAsNoTracking()
                .Include(pw => pw.TournamentTable)
                    .ThenInclude(tt => tt.TeamTableResults)
                    .ThenInclude(ttr => ttr.Team)
                .Where(pw => pw.Id == request.PlayWeekId)
                .SelectMany(x => x.TournamentTable.TeamTableResults.Where(ttr => !ttr.IsDropped).Select(e => e.Team))
                .ProjectTo<TeamsSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new CreateMatchCommand { Teams = teams, TournamentTableId = request.TournamentTableId, PlayWeekId = request.PlayWeekId };
            return viewModel;
        }
    }
}
