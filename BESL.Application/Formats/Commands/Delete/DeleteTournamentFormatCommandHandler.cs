namespace BESL.Application.Formats.Commands.Delete
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

    public class DeleteTournamentFormatCommandHandler : IRequestHandler<DeleteTournamentFormatCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteTournamentFormatCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteTournamentFormatCommand request, CancellationToken cancellationToken)
        {

            var desiredFormat = await this.dbContext
                .TournamentFormats
                .SingleOrDefaultAsync(g => g.Id == request.Id);

            if (desiredFormat == null)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.Id);
            }

            if (desiredFormat.IsDeleted)
            {
                throw new DeleteFailureException(nameof(TournamentFormat), desiredFormat.Id, ENTITY_ALREADY_DELETED_MSG);
            }
            else
            {
                desiredFormat.IsDeleted = true;
                desiredFormat.DeletedOn = DateTime.UtcNow;
                await this.dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
