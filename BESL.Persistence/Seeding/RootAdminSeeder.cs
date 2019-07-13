namespace BESL.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static BESL.Common.GlobalConstants;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;

    public class RootAdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider
              .GetRequiredService<RoleManager<PlayerRole>>();

            var userManager = serviceProvider
                .GetRequiredService<UserManager<Player>>();

            await SeedRootAdminAsync(userManager, roleManager, Role.Administrator.ToString());
        }

        private static async Task SeedRootAdminAsync(UserManager<Player> userManager, RoleManager<PlayerRole> roleManager, string roleName)
        {
            var result = await userManager
                .CreateAsync(new Player() { UserName = ADMIN_USERNAME, Email = ADMIN_EMAIL }, ADMIN_PASSWORD);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
            var rootAdminUser = await userManager.FindByNameAsync(ADMIN_USERNAME);

            var addToRoleResult = await userManager.AddToRoleAsync(rootAdminUser, Role.Administrator.ToString());

            if (!addToRoleResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(Environment.NewLine, addToRoleResult.Errors.Select(e => e.Description)));
            }
        }
    }
}
