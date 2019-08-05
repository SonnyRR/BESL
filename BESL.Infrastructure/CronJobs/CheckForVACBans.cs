namespace BESL.Infrastructure.CronJobs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Infrastructure.SteamWebAPI2;
    using BESL.Infrastructure.SteamWebAPI2.Interfaces;
    using static BESL.Common.GlobalConstants;

    public class CheckForVACBans
    {
        public const string CRON_SCHEDULE = "0 0 1 * * ?";

        private readonly ISteamUser mainSteamUserInstance;
        private readonly IDeletableEntityRepository<Player> playerRepository;

        public CheckForVACBans(IDeletableEntityRepository<Player> playerRepository, IConfiguration configuration)
        {
            this.playerRepository = playerRepository;
            this.mainSteamUserInstance = SteamApiHelper.GetSteamUserInstance(configuration);
        }

        public async Task CheckForBans()
        {
            var players = await this.playerRepository
                .AllAsNoTracking()
                .Include(p => p.Claims)
                .ToListAsync();

            var vacBannedPlayers = new List<Player>();

            foreach (var player in players)
            {
                var claim = player
                    .Claims
                    .SingleOrDefault(x => x.ClaimType == STEAM_ID_64_CLAIM_TYPE);

                if (claim == null)
                {
                    continue;
                }

                var currentPlayerId = ulong.Parse(claim.ClaimValue);

                var bans = await this.mainSteamUserInstance.GetPlayerBansAsync(currentPlayerId);
                var isBanned = bans.Data.Any(b => b.VACBanned == true);

                if (isBanned)
                {
                    if (!player.Claims.Any(c => c.ClaimType == IS_VAC_BANNED_CLAIM_TYPE))
                    {
                        var vacBanClaim = new IdentityUserClaim<string>()
                        {
                            ClaimType = IS_VAC_BANNED_CLAIM_TYPE,
                            ClaimValue = "true",
                            UserId = player.Id
                        };

                        player.Claims.Add(vacBanClaim);
                        vacBannedPlayers.Add(player);
                    }
                }

                await this.UpdatePlayers(vacBannedPlayers);
            }
        }
        
        private async Task<int> UpdatePlayers(IEnumerable<Player> bannedPlayers)
        {
            foreach (var player in bannedPlayers)
            {
                this.playerRepository.Update(player);
            }

            return await this.playerRepository.SaveChangesAsync();
        }
    }
}
