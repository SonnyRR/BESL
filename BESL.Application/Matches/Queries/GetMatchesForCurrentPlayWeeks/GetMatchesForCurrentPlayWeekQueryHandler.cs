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
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public GetMatchesForCurrentPlayWeeksQueryHandler(IDeletableEntityRepository<Match> matchesRepository, IMapper mapper)
        {
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
        }

        public async Task<MatchesForCurrentPlayWeeksViewModel> Handle(GetMatchesForCurrentPlayWeeksQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var matchLookups = await matchesRepository
                .AllAsNoTracking()
                .Where(x => x.PlayWeek.IsActive)
                .OrderBy(x => x.ScheduledDate)
                .ProjectTo<MatchLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new MatchesForCurrentPlayWeeksViewModel { Matches = matchLookups };

            return viewModel;
        }
    }
}
