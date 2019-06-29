namespace BESL.Application.Games.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using BESL.Application.Exceptions;
    using BESL.Domain.Entities;

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
    {
        private readonly IApplicationDbContext context;

        public DeleteGameCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            bool isDeleteSuccessfull = false;

            var desiredGame = await this.context
                .Games
                .SingleOrDefaultAsync(g => g.Id == request.Id);

            if (desiredGame == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            if (desiredGame.IsDeleted)
            {
                throw new DeleteFailureException(nameof(Game), desiredGame.Id, "Entity is already deleted.");
            }
            else
            {
                desiredGame.IsDeleted = true;
                desiredGame.DeletedOn = DateTime.UtcNow;
                isDeleteSuccessfull = true;
                await this.context.SaveChangesAsync(cancellationToken);
            }

            return isDeleteSuccessfull;
        }
    }
}
