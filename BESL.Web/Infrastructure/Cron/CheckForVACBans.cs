namespace BESL.Web.Infrastructure.Cron
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using SteamWebAPI2.Interfaces;

    using BESL.Common.SteamWebApi;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class CheckForVACBans
    {
        public const string CRON_SCHEDULE = "0 */2 * ? * *";

        private readonly UserManager<Player> userManager;
        private readonly ISteamUser mainSteamUserInstance;

        public CheckForVACBans(UserManager<Player> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mainSteamUserInstance = SteamApiHelper.GetSteamUserInstance(configuration);
        }

        public async Task CheckForBans()    
        {
            var players = await this.userManager
                .Users
                .ToListAsync();

            foreach (var player in players)
            {
                var claims = await this.userManager.GetClaimsAsync(player);
                var claim = claims.SingleOrDefault(x => x.Type == STEAM_ID_64_CLAIM_TYPE);

                if (claim == null)
                {
                    continue;
                }

                var currentPlayerId = ulong.Parse(claim.Value);

                var bans = await this.mainSteamUserInstance.GetPlayerBansAsync(currentPlayerId);
                var isBanned = bans.Data.Any(b => b.VACBanned == true);

                if (isBanned)
                {
                    if (!claims.Any(c => c.Type == IS_VAC_BANNED_CLAIM_TYPE))
                    {
                        await this.userManager.AddClaimAsync(player, new Claim(IS_VAC_BANNED_CLAIM_TYPE, "true"));
                    }
                }
            }
        }
    }
}
