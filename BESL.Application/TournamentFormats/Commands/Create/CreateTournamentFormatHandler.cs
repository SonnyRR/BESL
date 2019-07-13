namespace BESL.Application.TournamentFormats.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using System;
    using System.Linq;

    public class CreateTournamentFormatHandler : IRequestHandler<CreateTournamentFormatCommand, int>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> repository;

        public CreateTournamentFormatHandler(IDeletableEntityRepository<TournamentFormat> repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTournamentFormatValidator();
            var validationResult = validator.Validate(request);

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (this.repository.AllAsNoTrackingWithDeleted().Any(tf => tf.GameId == request.GameId && tf.Name == request.Name))
            {
                throw new EntityAlreadyExists(nameof(TournamentFormat), $"{request.Name} - GameId:{request.GameId}");
            }

            var game = await this.repository
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

            await this.repository.AddAsync(format);
            return await this.repository.SaveChangesAsync(cancellationToken);
        }
    }
}
