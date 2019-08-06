namespace BESL.Application.Infrastructure
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public static class CommonCheckHelper
    {
        public static async Task<bool> CheckIfUserExists(string userId, IDeletableEntityRepository<Player> playersRepository)
        {
            return await playersRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == userId);
        }
    }
}
