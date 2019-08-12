namespace BESL.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Infrastructure.SteamWebAPI2;
    using static BESL.Common.GlobalConstants;

    public class UsersSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
                .GetRequiredService<UserManager<Player>>();

            var dummyPassword = "1234567890Abcdefg";

            ulong steamId64 = 76561198041495727;
            var steamApi = SteamApiHelper.GetSteamUserInstance(serviceProvider.GetRequiredService<IConfiguration>());

            int desiredAccounts = 10;
            while (desiredAccounts >= 0)
            {
                var userSummary = await steamApi.GetPlayerSummaryAsync(steamId64);
                var username = userSummary.Data.Nickname;

                if (!string.IsNullOrWhiteSpace(username) && username.All(x => char.IsLetterOrDigit(x)))
{
                    var result = await userManager
                       .CreateAsync(new Player() { UserName = username, Email = $"{username}@fake.bg" }, dummyPassword);

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }

                    var currentUser = await userManager.FindByNameAsync(username);

                    currentUser.Claims.Add(new IdentityUserClaim<string>() { ClaimType = PROFILE_AVATAR_CLAIM_TYPE, ClaimValue = userSummary.Data.AvatarFullUrl, UserId = currentUser.Id });
                    currentUser.Claims.Add(new IdentityUserClaim<string>() { ClaimType = PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, ClaimValue = userSummary.Data.AvatarMediumUrl, UserId = currentUser.Id });

                    desiredAccounts--;
                }
                    steamId64 += 20;
            }
         }
    }
}
