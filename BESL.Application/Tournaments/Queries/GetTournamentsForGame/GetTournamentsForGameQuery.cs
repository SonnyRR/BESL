namespace BESL.Application.Tournaments.Queries.GetTournamentsForGame
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Common.Models.View;
    using BESL.Application.Common.Models.Lookups;

    public class GetTournamentsForGameQuery : IRequest<AllTournamentsViewModel>
    {
        public int GameId { get; set; }

        public class Handler : IRequestHandler<GetTournamentsForGameQuery, AllTournamentsViewModel>
        {
            private readonly IDeletableEntityRepository<Tournament> repository;
            private readonly IMapper mapper;

            public Handler(IDeletableEntityRepository<Tournament> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<AllTournamentsViewModel> Handle(GetTournamentsForGameQuery request, CancellationToken cancellationToken)
            {
                var lookupModels = await this.repository
                    .AllAsNoTracking()
                        .Include(t => t.Format)
                    .Where(t => t.Format.GameId == request.GameId)
                    .ProjectTo<TournamentLookupModel>(this.mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var viewModel = new AllTournamentsViewModel() { Tournaments = lookupModels };
                return viewModel;
            }
        }
    }
}
