namespace BESL.Application.Games.Queries.Details
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
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IMapper mapper;

        public GetGameDetailsQueryHandler(IDeletableEntityRepository<Game> gamesRepository, IMapper mapper)
        {
            this.gamesRepository = gamesRepository;
            this.mapper = mapper;
        }

        public async Task<GameDetailsViewModel> Handle(GetGameDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var gameDomain = await this.gamesRepository
                .AllAsNoTracking()
                .Include(g => g.TournamentFormats)
                    .ThenInclude(tf => tf.Teams)
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Game), request.Id);

            var lookupModel = this.mapper.Map<GameDetailsLookupModel>(gameDomain);

            var viewModel = new GameDetailsViewModel()
            {
                Id = lookupModel.Id,
                Name = lookupModel.Name,
                Description = lookupModel.Description,
                Tournaments = lookupModel.Tournaments,
                GameImageUrl = lookupModel.GameImageUrl
            };

            return viewModel;
        }
    }
}
