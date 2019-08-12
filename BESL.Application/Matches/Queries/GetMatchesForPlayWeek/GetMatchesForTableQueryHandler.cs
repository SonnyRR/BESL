namespace BESL.Application.Matches.Queries.GetMatchesForPlayWeek
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

    public class GetMatchesForPlayWeekQueryHandler : IRequestHandler<GetMatchesForPlayWeekQuery, MatchesForPlayWeekViewModel>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public GetMatchesForPlayWeekQueryHandler(IDeletableEntityRepository<Match> matchesRepository, IMapper mapper)
        {
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
        }

        public async Task<MatchesForPlayWeekViewModel> Handle(GetMatchesForPlayWeekQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var matches = await this.matchesRepository
                .AllAsNoTracking()
                .Where(m => m.PlayWeekId == request.PlayWeekId)
                .ProjectTo<MatchLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new MatchesForPlayWeekViewModel { Matches = matches, PlayWeekId = request.PlayWeekId };
            return viewModel;
        }
    }
}
