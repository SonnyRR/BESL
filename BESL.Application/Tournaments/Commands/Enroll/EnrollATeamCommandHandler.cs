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
        private readonly IUserAccessor userAccessor;

        public EnrollATeamCommandHandler(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<TournamentTable> tournamentTablesRepository,
            IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository,
            IUserAccessor userAccessor)
        {
            this.teamsRepository = teamsRepository;
            this.playersRepository = playersRepository;
            this.tournamentTablesRepository = tournamentTablesRepository;
            this.teamTableResultsRepository = teamTableResultsRepository;
            this.userAccessor = userAccessor;
        }

        public async Task<int> Handle(EnrollATeamCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfPlayerExists(this.userAccessor.UserId, playersRepository))
            {
                throw new NotFoundException(nameof(Player), this.userAccessor.UserId);
            }

            var desiredTable = await this.tournamentTablesRepository
                .AllAsNoTracking()
                .Include(tt => tt.TeamTableResults)
                .Include(ttr => ttr.Tournament)
                .SingleOrDefaultAsync(tt => tt.Id == request.TableId, cancellationToken)
                ?? throw new NotFoundException(nameof(TournamentTable), request.TableId);

            if (await CommonCheckHelper.CheckIfPlayerHasAlreadyEnrolledATeam(this.userAccessor.UserId, desiredTable.Tournament.FormatId, teamsRepository))
            {
                throw new PlayerHasAlreadyEnrolledTeamException();
            }

            if (await this.CheckIfTournamentTableIsFull(request.TableId, cancellationToken))
            {
                throw new TournamentTableIsFullException();
            }

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TeamTableResults)
                    .ThenInclude(ttr => ttr.TournamentTable)
                    .ThenInclude(tt => tt.Tournament)
                .Include(t => t.PlayerTeams)
                .SingleAsync(t => t.Id == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.TeamId);

            if (!await this.CheckIfTeamToEnrollHasTheCorrectFormat(desiredTeam.Id, desiredTable.Tournament.FormatId, cancellationToken))
            {
                throw new TeamFormatDoesNotMatchTournamentFormatException();
            }

            // This is commented out for testing purposes.

            //if (!await this.CheckIfTeamToEnrollHasTheMinimumRequiredPlayers(desiredTeam.Id, desiredTable.Tournament.FormatId, cancellationToken))
            //{
            //    throw new TeamDoesNotHaveEnoughPlayersException(desiredTeam.Name);
            //}

            var tableResult = new TeamTableResult() { TeamId = request.TeamId, TournamentTableId = desiredTable.Id };
            desiredTable.TeamTableResults.Add(tableResult);

            this.tournamentTablesRepository.Update(desiredTable);
            return await this.tournamentTablesRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task<bool> CheckIfTournamentTableIsFull(int tableId, CancellationToken cancellationToken)
        {
            var desiredTable = await this.tournamentTablesRepository
                .AllWithDeleted()
                .Include(tt => tt.TeamTableResults)
                .Select(x => new { Id = x.Id, TablesCount = x.TeamTableResults.Count, MaxNumberOfTeams = x.MaxNumberOfTeams })
                .FirstOrDefaultAsync(tt => tt.Id == tableId, cancellationToken);

            return desiredTable.TablesCount > desiredTable.MaxNumberOfTeams;
        }

        private async Task<bool> CheckIfTeamToEnrollHasTheCorrectFormat(int teamId, int formatId, CancellationToken cancellationToken)
        {
            return await this.teamsRepository
                 .AllAsNoTracking()
                 .Where(t => t.Id == teamId)
                 .Select(t => t.TournamentFormatId)
                 .SingleOrDefaultAsync(cancellationToken)
                 == formatId;
        }

        private async Task<bool> CheckIfTeamToEnrollHasTheMinimumRequiredPlayers(int teamId, int formatId, CancellationToken cancellationToken)
        {
            var desiredTeam = await this.teamsRepository
                 .AllAsNoTracking()
                 .Where(t => t.Id == teamId)
                 .Include(t => t.PlayerTeams)
                 .Include(t => t.TournamentFormat)
                 .SingleOrDefaultAsync(t => t.Id == teamId, cancellationToken);

            return desiredTeam.PlayerTeams.Count(pt => !pt.IsDeleted) >= desiredTeam.TournamentFormat.TeamPlayersCount;
        }

    }
}
