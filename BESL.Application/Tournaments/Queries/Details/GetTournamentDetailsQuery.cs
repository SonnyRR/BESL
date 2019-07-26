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

    public class GetTournamentDetailsQuery : IRequest<GetTournamentDetailsViewModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetTournamentDetailsQuery, GetTournamentDetailsViewModel>
        {
            private readonly IDeletableEntityRepository<Tournament> tournamentRepository;
            private readonly IMapper mapper;

            public Handler(IDeletableEntityRepository<Tournament> tournamentRepository, IMapper mapper)
            {
                this.tournamentRepository = tournamentRepository;
                this.mapper = mapper;
            }

            public async Task<GetTournamentDetailsViewModel> Handle(GetTournamentDetailsQuery request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var desiredTournament = await this.tournamentRepository
                    .AllAsNoTrackingWithDeleted()
                    .Include(t => t.Format)
                        .ThenInclude(tf => tf.Game)
                    .Include(t => t.Tables)
                        .ThenInclude(tb => tb.TeamTableResults)
                            .ThenInclude(ttr => ttr.Team)
                    .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                var viewModel = this.mapper.Map<GetTournamentDetailsViewModel>(desiredTournament);
                return viewModel;
            }
        }
    }
}
