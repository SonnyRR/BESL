using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BESL.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BESL.Application.Games.Queries.GetGameDetails
{
    public class GameDetailsQueryHandler : IRequestHandler<GameDetailsQuery, GameDetailsViewModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GameDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GameDetailsViewModel> Handle(GameDetailsQuery request, CancellationToken cancellationToken)
        {
            var gameDomain = await this.context.Games.FirstOrDefaultAsync(g => g.Id == request.Id);
            var dto = this.mapper.Map<GameDetailsLookupModel>(gameDomain);

            var viewModel = new GameDetailsViewModel()
            {
                Name = dto.Name,
                Description = dto.Description,
                Tournaments = dto.Tournaments,
                RegisteredTeams = dto.RegisteredTeams
            };

            return viewModel;
        }
    }
}
