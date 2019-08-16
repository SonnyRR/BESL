namespace BESL.Application.Matches.Queries.Details
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

    public class GetMatchDetailsQueryHandler : IRequestHandler<GetMatchDetailsQuery, GetMatchDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public GetMatchDetailsQueryHandler(IDeletableEntityRepository<Match> matchesRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            this.matchesRepository = matchesRepository;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<GetMatchDetailsViewModel> Handle(GetMatchDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.HomeTeam)
                    .ThenInclude(t => t.TournamentFormat)
                    .Include(x => x.ParticipatedPlayers)
                .Include(m => m.AwayTeam)
                    .ThenInclude(t => t.TournamentFormat)
                    .Include(x => x.ParticipatedPlayers)
                .Include(m => m.PlayWeek)
                .SingleOrDefaultAsync(m => m.Id == request.Id)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            var viewModel = this.mapper.Map<GetMatchDetailsViewModel>(desiredMatch, opts => opts.Items.Add("CurrentUserId", this.userAccessor.UserId));

            return viewModel;
        }
    }
}
