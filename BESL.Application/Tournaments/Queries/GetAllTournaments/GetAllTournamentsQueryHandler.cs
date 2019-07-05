namespace BESL.Application.Tournaments.Queries.GetAllTournaments
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;

    public class GetAllTournamentsQueryHandler : IRequestHandler<GetAllTournamentsQuery, AllTournamentsViewModel>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllTournamentsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AllTournamentsViewModel> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournamentsMapped = await this.dbContext
                .Tournaments
                    .Include(t => t.Game)
                    .Include(t => t.Format)
                .Where(t => !t.IsDeleted)
                .ProjectTo<TournamentLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new AllTournamentsViewModel()
            {
                Tournaments = tournamentsMapped
            };

            return viewModel;
        }
    }
}
