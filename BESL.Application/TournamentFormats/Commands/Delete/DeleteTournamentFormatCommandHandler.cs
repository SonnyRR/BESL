namespace BESL.Application.TournamentFormats.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class DeleteTournamentFormatCommandHandler : IRequestHandler<DeleteTournamentFormatCommand>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> repository;

        public DeleteTournamentFormatCommandHandler(IDeletableEntityRepository<TournamentFormat> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            var desiredFormat = await this.repository
                .GetByIdWithDeletedAsync(request.Id);

            if (desiredFormat == null)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.Id);
            }

            if (desiredFormat.IsDeleted)
            {
                throw new DeleteFailureException(nameof(TournamentFormat), desiredFormat.Id, ENTITY_ALREADY_DELETED_MSG);
            }

            this.repository.Delete(desiredFormat);
            await this.repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
