namespace BESL.Web.CronJobs
{
    using BESL.Common.SteamWebApi;
    using BESL.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using SteamWebAPI2.Interfaces;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using static BESL.Common.GlobalConstants;

    public class CheckForVACBans
    {
        public const string CRON_SCHEDULE = "0 */2 * ? * *";

        private readonly UserManager<Player> userManager;
        private readonly IConfiguration configuration;
        private readonly ISteamUser mainSteamUserInstance;

        public CheckForVACBans(UserManager<Player> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            mainSteamUserInstance = SteamApiHelper.GetSteamUserInstance(configuration);
        }

        public async Task CheckForBans()
        {
            var players = await this.userManager
                .Users
                .ToListAsync();

            foreach (var player in players)
            {
                var id = player.Claims.SingleOrDefault(x => x.ClaimType == STEAM_ID_64_CLAIM_TYPE);

                if (id == null)
                    continue;

                var currentPlayerId = ulong.Parse(id.ClaimValue);

                var bans = await mainSteamUserInstance.GetPlayerBansAsync(currentPlayerId);
                var isBanned = bans.Data.Any(b => b.VACBanned == true);

                if (isBanned)
                {
                    await this.userManager.AddClaimAsync(player, new Claim(IS_VAC_BANNED_CLAIM_TYPE, "true"));
                }
            }
        }
    }
}
