namespace BESL.Application.Games.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, int>
    {
        private readonly IDeletableEntityRepository<Game> gameRepository;

        public DeleteGameCommandHandler(IDeletableEntityRepository<Game> gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<int> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredGame = await this.gameRepository
                .AllWithDeleted()
                .SingleOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Game), request.Id);
       
            if (desiredGame.IsDeleted)
            {
                throw new DeleteFailureException(nameof(Game), desiredGame.Id, ENTITY_ALREADY_DELETED_MSG);
            }

            this.gameRepository.Delete(desiredGame);
            return await this.gameRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
