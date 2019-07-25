namespace BESL.Application.Tournaments.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentRepository;

        public DeleteTournamentCommandHandler(IDeletableEntityRepository<Tournament> tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        public async Task<Unit> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredTournament = await this.tournamentRepository
                .GetByIdWithDeletedAsync(request.Id);

            if (desiredTournament == null)
            {
                throw new NotFoundException(nameof(Tournament), request.Id);
            }

            this.tournamentRepository.Delete(desiredTournament);
            await this.tournamentRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
