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
        private readonly IDeletableEntityRepository<Game> gameRepository;

        public ModifyGameQueryHandler(IDeletableEntityRepository<Game> gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<ModifyGameCommand> Handle(ModifyGameQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredGame = await this.gameRepository
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
