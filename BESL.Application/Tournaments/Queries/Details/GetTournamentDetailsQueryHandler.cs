namespace BESL.Application.Tournaments.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Entities;

    public class GetTournamentDetailsQueryHandler : IRequestHandler<GetTournamentDetailsQuery, GetTournamentDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public GetTournamentDetailsQueryHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
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
                        .   ThenInclude(ttr => ttr.PlayerTeams)
                .SingleOrDefaultAsync(t => t.Id == request.Id && !t.Format.IsDeleted && !t.Format.Game.IsDeleted, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.Id);

            var viewModel = this.mapper.Map<GetTournamentDetailsViewModel>(desiredTournament, opts => opts.Items["CurrentUserId"] = this.userAccessor.UserId);

            return viewModel;
        }
    }
}
