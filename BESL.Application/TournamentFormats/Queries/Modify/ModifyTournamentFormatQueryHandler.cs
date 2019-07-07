namespace BESL.Application.TournamentFormats.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using BESL.Application.Common.Models;
    using AutoMapper.QueryableExtensions;
    using BESL.Application.Games.Queries.GetAllGamesSelectList;

    public class ModifyTournamentFormatQueryHandler : IRequestHandler<ModifyTournamentFormatQuery, ModifyTournamentFormatViewModel>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ModifyTournamentFormatQueryHandler(IApplicationDbContext dbContext, IMapper mapper, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<ModifyTournamentFormatViewModel> Handle(ModifyTournamentFormatQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredFormat = await this.dbContext
                .TournamentFormats
                    .Include(tf => tf.Game)
                .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (desiredFormat == null)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.Id);
            }

            var viewModel = new ModifyTournamentFormatViewModel()
            {
                Id = desiredFormat.Id,
                Name = desiredFormat.Name,
                Description = desiredFormat.Description,
                TeamPlayersCount = desiredFormat.TeamPlayersCount,
                GameName = desiredFormat.Game.Name,
                GameId = desiredFormat.Game.Id
            };

            return viewModel;
        }
    }
}
