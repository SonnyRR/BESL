namespace BESL.Application.Tournaments.Queries.GetTournamentsForGame
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetTournamentsForGameQueryHandler : IRequestHandler<GetTournamentsForGameQuery, GameTournamentsViewModel>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public GetTournamentsForGameQueryHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, IMapper mapper)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<GameTournamentsViewModel> Handle(GetTournamentsForGameQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var lookupModels = await this.tournamentsRepository
                .AllAsNoTracking()
                    .Include(t => t.Format)
                .Where(t => t.Format.GameId == request.GameId)
                .OrderByDescending(t => t.IsActive)
                .ThenBy(t => t.AreSignupsOpen)
                .ThenByDescending(t => t.CreatedOn)
                .ProjectTo<TournamentLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new GameTournamentsViewModel { Tournaments = lookupModels };
            return viewModel;
        }
    }
}
