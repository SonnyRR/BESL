﻿namespace BESL.Application.Games.Queries.GetGameDetails
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using System;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;

    public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDetailsViewModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetGameDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var gameDomain = await this.context.Games.FirstOrDefaultAsync(g => g.Id == request.Id);

            if (gameDomain == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            var dto = this.mapper.Map<GameDetailsLookupModel>(gameDomain);

            var viewModel = new GameDetailsViewModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Tournaments = dto.Tournaments,
                RegisteredTeams = dto.RegisteredTeams,
                GameImageUrl = dto.GameImageUrl
            };

            return viewModel;
        }
    }
}