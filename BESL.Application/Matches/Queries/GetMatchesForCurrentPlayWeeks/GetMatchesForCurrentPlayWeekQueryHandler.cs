namespace BESL.Application.Matches.Queries.GetMatchesForCurrentPlayWeeks
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

    public class GetMatchesForCurrentPlayWeeksQueryHandler : IRequestHandler<GetMatchesForCurrentPlayWeeksQuery, MatchesForCurrentPlayWeeksViewModel>
    {
        private readonly IDeletableEntityRepository<PlayWeek> playWeeksRepository;
        private readonly IMapper mapper;

        public GetMatchesForCurrentPlayWeeksQueryHandler(IDeletableEntityRepository<PlayWeek> playWeeksRepository, IMapper mapper)
        {
            this.playWeeksRepository = playWeeksRepository;
            this.mapper = mapper;
        }

        public async Task<MatchesForCurrentPlayWeeksViewModel> Handle(GetMatchesForCurrentPlayWeeksQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentMachineTime = DateTime.UtcNow.Date;

            var matchLookups = await this.playWeeksRepository
                .AllAsNoTracking()
                .Where(pw => pw.IsActive && pw.TournamentTable.Tournament.IsActive)
                .SelectMany(pw => pw.MatchFixtures)
                .OrderBy(m => m.ScheduledDate)
                .ProjectTo<MatchLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new MatchesForCurrentPlayWeeksViewModel { Matches = matchLookups };

            return viewModel;
        }
    }
}
