namespace BESL.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    public class UsersSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
                .GetRequiredService<UserManager<Player>>();

            for (int i = 1; i <= 36; i++)
            {
                var result = await userManager.CreateAsync(new Player
                {
                    UserName = $"FooPlayer{i}",
                    Email = $"FooBar@besl.bg"
                }, "Asdfgh2");


                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                var createdUser = await userManager.FindByNameAsync($"FooPlayer{i}");

                createdUser.Claims.Add(new IdentityUserClaim<string> { ClaimType = PROFILE_AVATAR_CLAIM_TYPE, ClaimValue = STEAM_DEFAULT_AVATAR_FULL, UserId = createdUser.Id });
                createdUser.Claims.Add(new IdentityUserClaim<string> { ClaimType = PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, ClaimValue = STEAM_DEFAULT_AVATAR_MEDIUM, UserId = createdUser.Id });
                createdUser.Claims.Add(new IdentityUserClaim<string> { ClaimType = STEAM_ID_64_CLAIM_TYPE, ClaimValue = "76561197960435530", UserId = createdUser.Id });
            }     
        }
    }
}
