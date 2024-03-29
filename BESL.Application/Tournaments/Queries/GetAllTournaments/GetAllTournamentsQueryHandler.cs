﻿namespace BESL.Application.Tournaments.Queries.GetAllTournaments
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
    using BESL.Entities;

    public class GetAllTournamentsQueryHandler : IRequestHandler<GetAllTournamentsQuery, AllTournamentsViewModel>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public GetAllTournamentsQueryHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, IMapper mapper)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<AllTournamentsViewModel> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var tournamentsMapped = await this.tournamentsRepository
                .AllAsNoTracking()
                .Include(t => t.Format)
                .Where(t => !t.Format.IsDeleted && !t.Format.Game.IsDeleted)
                .Where(t => t.IsActive != false || t.AreSignupsOpen != false)
                .ProjectTo<TournamentLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new AllTournamentsViewModel
            {
                Tournaments = tournamentsMapped
            };

            return viewModel;
        }
    }
}
