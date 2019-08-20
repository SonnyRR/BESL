namespace BESL.Application.TournamentFormats.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class CreateTournamentFormatCommandHandler : IRequestHandler<CreateTournamentFormatCommand, int>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> formatsRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public CreateTournamentFormatCommandHandler(
            IDeletableEntityRepository<TournamentFormat> formatsRepository, 
            IDeletableEntityRepository<Game> gamesRepository)
        {
            this.formatsRepository = formatsRepository;
            this.gamesRepository = gamesRepository;
        }

        public async Task<int> Handle(CreateTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await this.CheckIfTournamentFormatAlreadyExists(request))
            {
                throw new EntityAlreadyExistsException(nameof(TournamentFormat), $"{request.Name} - GameId:{request.GameId}");
            }

            var game = await this.gamesRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == request.GameId, cancellationToken)
                ?? throw new NotFoundException(nameof(Game), request.GameId);

            var format = new TournamentFormat()
            {
                Name = request.Name,
                Description = request.Description,
                TeamPlayersCount = request.TeamPlayersCount,
                TotalPlayersCount = request.TeamPlayersCount * TOURNAMENT_FORMAT_PLAYERS_MULTIPLIER,
                GameId = game.Id,
            };

            await this.formatsRepository.AddAsync(format);
            await this.formatsRepository.SaveChangesAsync(cancellationToken);

            return format.Id;
        }

        private async Task<bool> CheckIfTournamentFormatAlreadyExists(CreateTournamentFormatCommand request)
        {
            return await this.formatsRepository
                .AllAsNoTrackingWithDeleted()
                .AnyAsync(tf => tf.GameId == request.GameId && tf.Name == request.Name);
        }
    }
}
