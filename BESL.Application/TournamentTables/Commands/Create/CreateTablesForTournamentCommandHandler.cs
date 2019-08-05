namespace BESL.Application.TournamentTables.Commands.Create
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

    public class CreateTablesForTournamentCommandHandler : IRequestHandler<CreateTablesForTournamentCommand, int>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;

        public CreateTablesForTournamentCommandHandler(IDeletableEntityRepository<Tournament> tournamentsRepository)
        {
            this.tournamentsRepository = tournamentsRepository;
        }

        public async Task<int> Handle(CreateTablesForTournamentCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.TournamentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.TournamentId);

            desiredTournament.Tables.Add(new TournamentTable() { Name = OPEN_TABLE_NAME, CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = OPEN_TABLE_MAX_TEAMS });
            desiredTournament.Tables.Add(new TournamentTable() { Name = MID_TABLE_NAME, CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = MID_TABLE_MAX_TEAMS });
            desiredTournament.Tables.Add(new TournamentTable() { Name = PREM_TABLE_NAME, CreatedOn = DateTime.UtcNow, MaxNumberOfTeams = PREM_TABLE_MAX_TEAMS });

            this.tournamentsRepository.Update(desiredTournament);
            return await this.tournamentsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
