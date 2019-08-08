namespace BESL.Application.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    internal static class CommonCheckHelper
    {
        internal static async Task<bool> CheckIfUserExists(string userId, IDeletableEntityRepository<Player> playersRepository)
        {
            return await playersRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == userId);
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
    }
}
