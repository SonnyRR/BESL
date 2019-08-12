namespace BESL.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;

    public class RoleSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<PlayerRole>>();

            await SeedRoleAsync(roleManager, Role.Administrator.ToString());
        }

        private static async Task SeedRoleAsync(RoleManager<PlayerRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                var result = await roleManager.CreateAsync(new PlayerRole(roleName));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
