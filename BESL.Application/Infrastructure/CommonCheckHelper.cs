namespace BESL.Application.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    internal static class CommonCheckHelper
    {
        internal static async Task<bool> CheckIfPlayerExists(string userId, IDeletableEntityRepository<Player> playersRepository)
        {
            return await playersRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == userId);
        }

        internal static async Task<bool> CheckIfPlayerIsVACBanned(string userId, IDeletableEntityRepository<Player> playersRepository)
        {
            return await playersRepository
                .AllAsNoTracking()
                .Where(p => p.Id == userId)
                .Include(p => p.Claims)
                .SelectMany(x => x.Claims)
                .AnyAsync(x => x.ClaimType == IS_VAC_BANNED_CLAIM_TYPE);
        }

        internal static async Task<bool> CheckIfPlayerHasAlreadyEnrolledATeam(string userId, int tournamentFormatId, IDeletableEntityRepository<Team> teamsRepository)
        {
            return await teamsRepository
                .AllAsNoTracking()
                .Where(t => t.OwnerId == userId && t.TournamentFormatId == tournamentFormatId)
                .Include(t => t.TeamTableResults)
                    .ThenInclude(ttr => ttr.TournamentTable)
                    .ThenInclude(tt => tt.Tournament)
                .SelectMany(x => x.TeamTableResults.Select(y => y.TournamentTable.Tournament))
                .AnyAsync(x => x.AreSignupsOpen || x.IsActive);
        }

        internal static async Task<bool> CheckIfPlayerIsAlreadyInATeamWithTheSameFormat(string playerId, int formatId, IDeletableEntityRepository<PlayerTeam> playerTeamsRepository)
        {
            return await playerTeamsRepository
                   .AllAsNoTrackingWithDeleted()
                   .Include(pt => pt.Team)
                   .Where(pt => pt.PlayerId == playerId && !pt.IsDeleted)
                   .AnyAsync(pt => pt.Team.TournamentFormatId == formatId);
        }

        internal static async Task<bool> CheckIfPlayerHasLinkedSteamAccount(string userId, IDeletableEntityRepository<Player> playersRepository)
        {
            return await playersRepository
                .AllAsNoTracking()
                .Where(p => p.Id == userId)
                .Include(p => p.Claims)
                .AnyAsync(p => p.Claims.Any(c => c.ClaimType == STEAM_ID_64_CLAIM_TYPE));
        }

        internal static async Task<bool> CheckIfTeamExists(int teamId, IDeletableEntityRepository<Team> teamsRepository)
        {
            return await teamsRepository
                .AllAsNoTracking()
                .AnyAsync(t => t.Id == teamId);
        }

        internal static async Task<bool> CheckIfTeamIsFull(int teamId, IDeletableEntityRepository<Team> teamsRepository)
        {
            var team = await teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                .Include(t => t.PlayerTeams)
                .SingleOrDefaultAsync(x => x.Id == teamId);

            var playersCount = team
                .PlayerTeams
                .Count(x => x.IsDeleted == false);

            var maxPlayers = team.TournamentFormat.TeamPlayersCount + TEAM_MAX_BACKUP_PLAYERS_COUNT;
            bool isFull = playersCount >= maxPlayers;

            return isFull;
        }

        internal static async Task<bool> CheckIfTournamentTableExists(int tournamentTableId, IDeletableEntityRepository<TournamentTable> tournamentTablesRepository)
        {
            return await tournamentTablesRepository
                .AllAsNoTracking()
                .AnyAsync(tt => tt.Id == tournamentTableId);
        }
    }
}
