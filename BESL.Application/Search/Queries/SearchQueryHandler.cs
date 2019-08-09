namespace BESL.Application.Search.Queries
{
    using AutoMapper;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

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

        public Task<SearchQueryViewModel> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            return null;
        }
    }
}
