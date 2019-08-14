namespace BESL.Application.Matches.Queries.GetMatchesForPlayWeek
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;
    using static BESL.Common.GlobalConstants;

    public class GetMatchesForPlayWeekQueryHandler : IRequestHandler<GetMatchesForPlayWeekQuery, MatchesForPlayWeekViewModel>
    {
        private readonly IDeletableEntityRepository<PlayWeek> playWeeksRepository;
        private readonly IMapper mapper;

        public GetMatchesForPlayWeekQueryHandler(IDeletableEntityRepository<PlayWeek> playWeeksRepository, IMapper mapper)
        {
            this.playWeeksRepository = playWeeksRepository;
            this.mapper = mapper;
        }

        public async Task<MatchesForPlayWeekViewModel> Handle(GetMatchesForPlayWeekQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfPlayWeekExists(request.PlayWeekId))
            {
                throw new NotFoundException(nameof(PlayWeek), request.PlayWeekId);
            }

            var playWeek = await this.playWeeksRepository
                .AllAsNoTracking()
                .Include(pw => pw.MatchFixtures)
                .SingleOrDefaultAsync(pw => pw.Id == request.PlayWeekId, cancellationToken)
                ?? throw new NotFoundException(nameof(PlayWeek), request.PlayWeekId);

            var matches = this.mapper.Map<IEnumerable<MatchLookupModel>>(playWeek.MatchFixtures);

            var viewModel = new MatchesForPlayWeekViewModel
            {
                Matches = matches,
                PlayWeekId = request.PlayWeekId,
                WeekAsString = $"{playWeek.StartDate.ToString(DATE_FORMAT)} - {playWeek.EndDate.ToString(DATE_FORMAT)}"
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
