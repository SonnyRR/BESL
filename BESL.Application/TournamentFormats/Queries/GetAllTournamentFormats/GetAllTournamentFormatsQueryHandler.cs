﻿namespace BESL.Application.TournamentFormats.Queries.GetAllTournamentFormats
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

    public class GetAllTournamentFormatsQueryHandler : IRequestHandler<GetAllTournamentFormatsQuery, GetAllTournamentFormatsQueryViewModel>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;
        private readonly IMapper mapper;

        public GetAllTournamentFormatsQueryHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository, IMapper mapper)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllTournamentFormatsQueryViewModel> Handle(GetAllTournamentFormatsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var tournamentFormatsLookups = await this.tournamentFormatsRepository
                .AllAsNoTracking()
                .Where(e => !e.Game.IsDeleted)
                .ProjectTo<TournamentFormatLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModel = new GetAllTournamentFormatsQueryViewModel(tournamentFormatsLookups);
            return viewModel;
        }
    }
}
