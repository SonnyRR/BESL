namespace BESL.Application.Tournaments.Commands.Enroll
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EnrollATeamCommandHandler : IRequestHandler<EnrollATeamCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<TournamentTable> tournamentTablesRepository;

        public EnrollATeamCommandHandler(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<TournamentTable> tournamentTablesRepository)
        {
            this.teamsRepository = teamsRepository;
            this.playersRepository = playersRepository;
            this.tournamentTablesRepository = tournamentTablesRepository;
        }

        public async Task<int> Handle(EnrollATeamCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfUserExists(request.UserId, playersRepository))
            {
                throw new NotFoundException(nameof(Player), request.UserId);
            }

            if (await this.CheckIfUserAlreadyHasAnEnrolledTeamInGivenTournament(request))
            {
                throw new PlayerHasAlreadyEnrolledTeamException();
            }

            if (await this.CheckIfTournamentTableIsFull(request))
            {
                throw new TournamentTableIsFullException();
            }

            var desiredTable = await this.tournamentTablesRepository
                .AllAsNoTracking()
                .Include(tt => tt.TeamTableResults)
                .SingleOrDefaultAsync(tt => tt.Id == request.TableId, cancellationToken)
                ?? throw new NotFoundException(nameof(TournamentTable), request.TableId);

            desiredTable.TeamTableResults.Add(new TeamTableResult { TeamId = request.TeamId });

            this.tournamentTablesRepository.Update(desiredTable);
            return await this.tournamentTablesRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task<bool> CheckIfUserAlreadyHasAnEnrolledTeamInGivenTournament(EnrollATeamCommand request)
        {
            return await this.teamsRepository
                .AllAsNoTracking()
                .Where(t => t.OwnerId == request.UserId)
                .Include(t => t.CurrentActiveTeamTableResult)
                    .ThenInclude(cattr => cattr.TournamentTable)
                .AnyAsync(t => t.CurrentActiveTeamTableResult.TournamentTable.TournamentId == request.TournamentId);
        }

        private async Task<bool> CheckIfTournamentTableIsFull(EnrollATeamCommand request)
        {
            var desiredTable = await this.tournamentTablesRepository
                .AllWithDeleted()
                .Include(tt => tt.TeamTableResults)
                .Select(x => new { Id = x.Id, TablesCount = x.TeamTableResults.Count, MaxNumberOfTeams = x.MaxNumberOfTeams })
                .FirstOrDefaultAsync(tt => tt.Id == request.TableId);

            return desiredTable.TablesCount > desiredTable.MaxNumberOfTeams;
        }

    }
}
