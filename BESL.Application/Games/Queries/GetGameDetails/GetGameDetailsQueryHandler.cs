namespace BESL.Application.Games.Queries.GetGameDetails
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;

    public class GetGameDetailsQueryHandler : IRequestHandler<GetGameDetailsQuery, GameDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Game> repository;
        private readonly IMapper mapper;

        public GetGameDetailsQueryHandler(IDeletableEntityRepository<Game> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var gameDomain = await this.repository
                .AllAsNoTracking()
                .Include(g => g.TournamentFormats)
                    .ThenInclude(tf => tf.Teams)
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

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
                GameImageUrl = dto.GameImageUrl
            };

            return viewModel;
        }
    }
}
