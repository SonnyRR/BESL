namespace BESL.Web.Middlewares
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using BESL.Persistence;

    public class SeedMiddleware
    {
        private readonly RequestDelegate next;

        public SeedMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            SeedRoles(roleManager).GetAwaiter().GetResult();

            SeedUserInRoles(userManager).GetAwaiter().GetResult();

            // Call the next delegate/middleware in the pipeline
            await this.next(context);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Role.Administrator.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Administrator.ToString()));
            }
        }

        private static async Task SeedUserInRoles(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "LeagueAdministrator",
                    Email = "admin@besl.com",
                };

                var password = "BanHammer1000%";

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
                }
            }
        }
    }
}
