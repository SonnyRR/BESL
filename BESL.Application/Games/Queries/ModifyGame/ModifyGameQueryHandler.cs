﻿namespace BESL.Application.Games.Queries.ModifyGame
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using System;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;

    public class ModifyGameQueryHandler : IRequestHandler<ModifyGameQuery, ModifyGameViewModel>
    {
        private readonly IApplicationDbContext context;

        public ModifyGameQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ModifyGameViewModel> Handle(ModifyGameQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredGame = await this.context
                .Games
                .SingleOrDefaultAsync(g => g.Id == request.Id);

            if (desiredGame == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            var viewModel = new ModifyGameViewModel()
            {
                Id = desiredGame.Id,
                Name = desiredGame.Name,
                Description = desiredGame.Description,
                GameImageUrl = desiredGame.GameImageUrl
            };

            return viewModel;
        }
    }
}
