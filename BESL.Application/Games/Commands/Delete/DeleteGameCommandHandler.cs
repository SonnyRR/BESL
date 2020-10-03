namespace BESL.Application.Games.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, int>
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IMediator mediator;

        public DeleteGameCommandHandler(IDeletableEntityRepository<Game> gamesRepository, IMediator mediator)
        {
            this.gamesRepository = gamesRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredGame = await this.gamesRepository
                .AllWithDeleted()
                .SingleOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Game), request.Id);
       
            if (desiredGame.IsDeleted)
            {
                throw new DeleteFailureException(nameof(Game), desiredGame.Id, ENTITY_ALREADY_DELETED_MSG);
            }

            this.gamesRepository.Delete(desiredGame);

            int rowsAffected = await this.gamesRepository.SaveChangesAsync(cancellationToken);
            await this.mediator.Publish(new GameDeletedNotification() { GameName = desiredGame.Name });

            return desiredGame.Id;
        }
    }
}
