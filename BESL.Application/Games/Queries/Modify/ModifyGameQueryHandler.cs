namespace BESL.Application.Games.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using BESL.Application.Games.Commands.Modify;

    public class ModifyGameQueryHandler : IRequestHandler<ModifyGameQuery, ModifyGameCommand>
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public ModifyGameQueryHandler(IDeletableEntityRepository<Game> gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public async Task<ModifyGameCommand> Handle(ModifyGameQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredGame = await this.gamesRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Game), request.Id);
            
            var viewModel = new ModifyGameCommand()
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
