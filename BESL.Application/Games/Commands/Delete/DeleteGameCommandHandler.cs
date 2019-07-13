namespace BESL.Application.Games.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Application.Exceptions;
    using static BESL.Common.GlobalConstants;
    using BESL.Domain.Entities;

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IDeletableEntityRepository<Game> repository;

        public DeleteGameCommandHandler(IDeletableEntityRepository<Game> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredGame = await this.repository.GetByIdWithDeletedAsync(request.Id);

            if (desiredGame == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            if (desiredGame.IsDeleted)
            {
                throw new DeleteFailureException(nameof(Game), desiredGame.Id, ENTITY_ALREADY_DELETED_MSG);
            }

            this.repository.Delete(desiredGame);
            await this.repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
