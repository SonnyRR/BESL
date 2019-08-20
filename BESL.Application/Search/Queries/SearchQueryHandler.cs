namespace BESL.Application.Search.Queries
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using MediatR;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchQueryViewModel>
    {
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public SearchQueryHandler(
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Tournament> tournamentsRepository,
            IMapper mapper)
        {
            this.playersRepository = playersRepository;
            this.teamsRepository = teamsRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<SearchQueryViewModel> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Query))
            {
                throw new InvalidSearchQueryException();
            }

            var queryNormalized = request.Query.ToLower();

            var players = await this.playersRepository
                .AllAsNoTracking()
                .Where(p => p.UserName.ToLower().Contains(queryNormalized))
                .OrderBy(p => p.UserName)
                .ProjectTo<PlayerLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var teams = await this.teamsRepository
                .AllAsNoTracking()
                .Where(p => p.Name.ToLower().Contains(queryNormalized))
                .OrderBy(p => p.Name)
                .ProjectTo<TeamLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var tournaments = await this.tournamentsRepository
              .AllAsNoTracking()
              .Where(p => p.Name.ToLower().Contains(queryNormalized))
              .OrderBy(p => p.Name)
              .ProjectTo<TournamentLookupModel>(this.mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);

            var viewModel = new SearchQueryViewModel
            {
                SearchQuery = request.Query,
                Players = players,
                Teams = teams,
                Tournaments = tournaments
            };

            return viewModel;
        }
    }
}
