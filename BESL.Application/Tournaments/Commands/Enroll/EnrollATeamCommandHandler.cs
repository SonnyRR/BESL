namespace BESL.Application.Tournaments.Commands.Enroll
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class EnrollATeamCommandHandler : IRequestHandler<EnrollATeamCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<TournamentTable> tournamentTablesRepository;
        private readonly IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository;

        public EnrollATeamCommandHandler(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<TournamentTable> tournamentTablesRepository,
            IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.playersRepository = playersRepository;
            this.tournamentTablesRepository = tournamentTablesRepository;
            this.teamTableResultsRepository = teamTableResultsRepository;
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
                .Include(tt => tt.Tournament)
                .SingleOrDefaultAsync(tt => tt.Id == request.TableId, cancellationToken)
                ?? throw new NotFoundException(nameof(TournamentTable), request.TableId);

            if (!await this.CheckIfTeamToEnrollHasTheCorrectFormat(request, desiredTable.Tournament.FormatId))
            {
                throw new TeamFormatDoesNotMatchTournamentFormatException();
            }

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .SingleAsync(t => t.Id == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(TournamentTable), request.TeamId);


            var tableResult = new TeamTableResult() { TeamId = request.TeamId, TournamentTableId = desiredTable.Id, IsActive = true };
            desiredTable.TeamTableResults.Add(tableResult);

            this.tournamentTablesRepository.Update(desiredTable);
            return await this.tournamentTablesRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task<bool> CheckIfUserAlreadyHasAnEnrolledTeamInGivenTournament(EnrollATeamCommand request)
        {
            return await this.teamTableResultsRepository
                .AllAsNoTracking()
                .Where(x => x.TeamId == request.TeamId)
                .AnyAsync(x => x.IsActive);
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

        private async Task<bool> CheckIfTeamToEnrollHasTheCorrectFormat(EnrollATeamCommand request, int formatId)
        {
            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.TeamId);

            return desiredTeam.TournamentFormatId == formatId;
        }
    }
}
