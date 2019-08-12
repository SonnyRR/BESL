namespace BESL.Application.Tournaments.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class GetTournamentDetailsQueryHandler : IRequestHandler<GetTournamentDetailsQuery, GetTournamentDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public GetTournamentDetailsQueryHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, IMapper mapper)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<GetTournamentDetailsViewModel> Handle(GetTournamentDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .Include(t => t.Format)
                    .ThenInclude(tf => tf.Game)
                .Include(t => t.Tables)
                    .ThenInclude(tb => tb.TeamTableResults)
                        .ThenInclude(ttr => ttr.Team)
                .SingleOrDefaultAsync(t => t.Id == request.Id && !t.Format.IsDeleted && !t.Format.Game.IsDeleted, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.Id);

            var viewModel = this.mapper.Map<GetTournamentDetailsViewModel>(desiredTournament);
            return viewModel;
        }
    }
}
