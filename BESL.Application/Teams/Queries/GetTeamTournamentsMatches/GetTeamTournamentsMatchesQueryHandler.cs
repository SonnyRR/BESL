namespace BESL.Application.Teams.Queries.GetTeamTournamentsMatches
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

    public class GetTeamTournamentsMatchesQueryHandler : IRequestHandler<GetTeamTournamentsMatchesQuery, GetTeamTournamentsMatchesViewModel>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public GetTeamTournamentsMatchesQueryHandler(IDeletableEntityRepository<Match> teamsRepository, IMapper mapper)
        {
            this.matchesRepository = teamsRepository;
            this.mapper = mapper;
        }

        public async Task<GetTeamTournamentsMatchesViewModel> Handle(GetTeamTournamentsMatchesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var tournamentMatches = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.PlayWeek)
                    .ThenInclude(pw => pw.TournamentTable)
                       .ThenInclude(tt => tt.Tournament)
                .Where(x => x.AwayTeamId == request.TeamId || x.HomeTeamId == request.TeamId)
                .GroupBy(x => x.PlayWeek.TournamentTable.Tournament.Name)
                .ProjectTo<TournamentMatchesLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new GetTeamTournamentsMatchesViewModel
            {
                TournamentMatches = tournamentMatches
            };

            return viewModel;
        }
    }
}
