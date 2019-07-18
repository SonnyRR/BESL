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
    using BESL.Domain.Entities;
    using BESL.Application.Common.Models.View;
    using BESL.Application.Common.Models.Lookups;

    public class GetAllTournamentsQueryHandler : IRequestHandler<GetAllTournamentsQuery, AllTournamentsViewModel>
    {
        private readonly IDeletableEntityRepository<Tournament> repository;
        private readonly IMapper mapper;

        public GetAllTournamentsQueryHandler(IDeletableEntityRepository<Tournament> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<AllTournamentsViewModel> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournamentsMapped = await this.repository
                .AllAsNoTracking()
                .Where(t => !t.Game.IsDeleted)
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
