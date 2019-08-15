namespace BESL.Application.PlayWeeks.Queries.GetPlayWeeksForTournamentTable
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class GetPlayWeeksForTournamentTableQueryHandler : IRequestHandler<GetPlayWeeksForTournamentTableQuery, PlayWeeksForTournamentTableViewModel>
    {
        private readonly IDeletableEntityRepository<PlayWeek> playWeeksRepository;
        private readonly IDeletableEntityRepository<TournamentTable> tournamentTablesRepository;
        private readonly IMapper mapper;

        public GetPlayWeeksForTournamentTableQueryHandler(
            IDeletableEntityRepository<PlayWeek> playWeeksRepository,
            IDeletableEntityRepository<TournamentTable> tournamentTablesRepository,
            IMapper mapper)
        {
            this.playWeeksRepository = playWeeksRepository;
            this.tournamentTablesRepository = tournamentTablesRepository;
            this.mapper = mapper;
        }

        public async Task<PlayWeeksForTournamentTableViewModel> Handle(GetPlayWeeksForTournamentTableQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfTournamentTableExists(request.TournamentTableId, this.tournamentTablesRepository))
            {
                throw new NotFoundException(nameof(TournamentTable), request.TournamentTableId);
            }

            var weeksForTournamentTableLookups = await this.playWeeksRepository
                .AllAsNoTracking()
                .Where(pw => pw.TournamentTableId == request.TournamentTableId)
                .ProjectTo<PlayWeekLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new PlayWeeksForTournamentTableViewModel { PlayWeeks = weeksForTournamentTableLookups };
            return viewModel;
        }
    }
}
