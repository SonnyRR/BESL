namespace BESL.Application.Matches.Queries.EditMatchResult
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class EditMatchResultsQueryHandler : IRequestHandler<EditMatchResultQuery, EditMatchResultQueryViewModel>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IUserAccessor userAccessor;
        private readonly IMapper mapper;

        public EditMatchResultsQueryHandler(IDeletableEntityRepository<Match> matchesRepository, IUserAccessor userAccessor, IMapper mapper)
        {
            this.matchesRepository = matchesRepository;
            this.userAccessor = userAccessor;
            this.mapper = mapper;
        }

        public async Task<EditMatchResultQueryViewModel> Handle(EditMatchResultQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .AllAsNoTracking()
                .Include(m => m.HomeTeam)
                    .ThenInclude(ht => ht.PlayerTeams)
                    .ThenInclude(pt => pt.Player)
                .Include(m => m.AwayTeam)
                    .ThenInclude(at => at.PlayerTeams)
                    .ThenInclude(at => at.Player)
                .SingleOrDefaultAsync(m => m.Id == request.Id)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            if (desiredMatch.HomeTeam.OwnerId != this.userAccessor.UserId
                && desiredMatch.AwayTeam.OwnerId != this.userAccessor.UserId)
            {
                throw new ForbiddenException();
            }

            var viewModel = this.mapper.Map<EditMatchResultQueryViewModel>(desiredMatch);

            return viewModel;
        }
    }
}
