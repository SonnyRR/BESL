namespace BESL.Application.Tournaments.Queries.GetAllTournamentsSelectList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetAllTournamentsSelectListQueryHandler : IRequestHandler<GetAllTournamentsSelectListQuery, IEnumerable<TournamentSelectItemLookupModel>>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public GetAllTournamentsSelectListQueryHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, IMapper mapper)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TournamentSelectItemLookupModel>> Handle(GetAllTournamentsSelectListQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var tournamentsMapped = await this.tournamentsRepository
                .AllAsNoTracking()
                    .Include(t => t.Format)
                .Where(t => !t.Format.IsDeleted && !t.Format.Game.IsDeleted)
                .Where(t => t.IsActive != false || t.AreSignupsOpen != false)
                .ProjectTo<TournamentSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return tournamentsMapped;
        }
    }
}
