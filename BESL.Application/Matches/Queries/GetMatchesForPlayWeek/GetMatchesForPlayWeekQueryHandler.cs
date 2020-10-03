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
    using BESL.Application.Exceptions;
    using BESL.Entities;
    using static BESL.Common.GlobalConstants;

    public class GetMatchesForPlayWeekQueryHandler : IRequestHandler<GetMatchesForPlayWeekQuery, MatchesForPlayWeekViewModel>
    {
        private readonly IDeletableEntityRepository<PlayWeek> playWeeksRepository;
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;

        public GetMatchesForPlayWeekQueryHandler(
            IDeletableEntityRepository<PlayWeek> playWeeksRepository,
            IDeletableEntityRepository<Match> matchesRepository,
            IMapper mapper)
        {
            this.playWeeksRepository = playWeeksRepository;
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
        }

        public async Task<MatchesForPlayWeekViewModel> Handle(GetMatchesForPlayWeekQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfPlayWeekExists(request.PlayWeekId))
            {
                throw new NotFoundException(nameof(PlayWeek), request.PlayWeekId);
            }

            var desiredPlayWeek = await this.playWeeksRepository
                .AllAsNoTracking()
                .Select(x => new { x.Id, x.StartDate, x.EndDate })
                .SingleOrDefaultAsync(pw => pw.Id == request.PlayWeekId)
                ?? throw new NotFoundException(nameof(PlayWeek), request.PlayWeekId);

            var matches = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.PlayWeekId == request.PlayWeekId)
                .OrderByDescending(m => m.CreatedOn)
                .ProjectTo<MatchLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new MatchesForPlayWeekViewModel
            {
                Matches = matches,
                PlayWeekId = request.PlayWeekId,
                WeekAsString = $"{desiredPlayWeek.StartDate.ToString(DATE_FORMAT)} - {desiredPlayWeek.EndDate.ToString(DATE_FORMAT)}"
            };

            return viewModel;
        }

        private async Task<bool> CheckIfPlayWeekExists(int id)
        {
            return await this.playWeeksRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
        }
    }
}
