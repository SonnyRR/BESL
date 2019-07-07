namespace BESL.Web
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using static BESL.Common.GlobalConstants;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using BESL.Persistence;

    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<IApplicationDbContext>();
                    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    var userManager = scope.ServiceProvider.GetService<UserManager<Player>>();

                    var concreteContext = (ApplicationDbContext)context;
                    //concreteContext.Database.Migrate();

                    if (!await roleManager.RoleExistsAsync(Role.Administrator.ToString()))
                    {
                        await roleManager.CreateAsync(new IdentityRole(Role.Administrator.ToString()));
                    }

                    if (!userManager.Users.Any())
                    {
                        var user = new Player
                        {
                            UserName = ADMIN_USERNAME,
                            Email = ADMIN_EMAIL,
                        };

                        var password = ADMIN_PASSWORD;

                        var result = await userManager.CreateAsync(user, password);

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
                        }
                    }

                    DbInitializer.Initialize(concreteContext);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
            }

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
