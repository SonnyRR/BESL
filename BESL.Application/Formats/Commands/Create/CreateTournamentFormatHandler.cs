namespace BESL.Application.Formats.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using System;

    public class CreateTournamentFormatHandler : IRequestHandler<CreateTournamentFormatCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateTournamentFormatHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Handle(CreateTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTournamentFormatValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var game = await this.dbContext
                .Games
                .SingleOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            var format = new TournamentFormat()
            {
                Name = request.Name,
                Description = request.Description,
                TeamPlayersCount = request.TeamPlayersCount,
                TotalPlayersCount = request.TotalPlayersCount,
                GameId = request.GameId,
                CreatedOn = DateTime.UtcNow
            };

            this.dbContext.TournamentFormats.Add(format);
            return await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
