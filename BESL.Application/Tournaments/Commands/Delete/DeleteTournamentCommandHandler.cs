namespace BESL.Application.Tournaments.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;

        public DeleteTournamentCommandHandler(IDeletableEntityRepository<Tournament> tournamentsRepository)
        {
            this.tournamentsRepository = tournamentsRepository;
        }

        public async Task<int> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.Id);

            this.tournamentsRepository.Delete(desiredTournament);
            return await this.tournamentsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
