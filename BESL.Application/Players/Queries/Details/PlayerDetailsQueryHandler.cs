﻿namespace BESL.Application.Players.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class PlayerDetailsQueryHandler : IRequestHandler<GetPlayerDetailsQuery, PlayerDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Player> repository;
        private readonly IMapper mapper;

        public PlayerDetailsQueryHandler(IDeletableEntityRepository<Player> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PlayerDetailsViewModel> Handle(GetPlayerDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredPlayer = await this.repository
                .AllAsNoTracking()
                .Include(p => p.Claims)
                .SingleOrDefaultAsync(p => p.UserName == request.Username, cancellationToken);

            if (desiredPlayer == null)
            {
                throw new NotFoundException(nameof(Player), request.Username);
            }

            var viewModel = this.mapper.Map<PlayerDetailsViewModel>(desiredPlayer);
            return viewModel;
        }
    }
}