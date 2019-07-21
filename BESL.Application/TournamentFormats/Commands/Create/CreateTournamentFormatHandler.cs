namespace BESL.Application.TournamentFormats.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class CreateTournamentFormatHandler : IRequestHandler<CreateTournamentFormatCommand, int>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> formatRepository;
        private readonly IDeletableEntityRepository<Game> gameRepository;

        public CreateTournamentFormatHandler(
            IDeletableEntityRepository<TournamentFormat> formatRepository, 
            IDeletableEntityRepository<Game> gameRepository)
        {
            this.formatRepository = formatRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<int> Handle(CreateTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (this.formatRepository.AllAsNoTrackingWithDeleted().Any(tf => tf.GameId == request.GameId && tf.Name == request.Name))
            {
                throw new EntityAlreadyExists(nameof(TournamentFormat), $"{request.Name} - GameId:{request.GameId}");
            }

            var game = await this.gameRepository
                .GetByIdWithDeletedAsync(request.GameId);              

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            var format = new TournamentFormat()
            {
                Name = request.Name,
                Description = request.Description,
                TeamPlayersCount = request.TeamPlayersCount,
                TotalPlayersCount = request.TeamPlayersCount * 2,
                GameId = request.GameId,
                CreatedOn = DateTime.UtcNow
            };

            await this.formatRepository.AddAsync(format);
            return await this.formatRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
