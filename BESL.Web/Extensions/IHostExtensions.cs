namespace BESL.Web.Extensions
{
    using System.Linq;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    
    using BESL.Persistence;
    using BESL.Persistence.Seeding;
    using BESL.SharedKernel;

    public static class IHostExtensions
    {
        public static IHost InitializeDatabase(this IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var env = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.EnvironmentName == GlobalConstants.ENV_DEVELOPMENT
                    || env.EnvironmentName == GlobalConstants.ENV_LOCAL)
                {
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
                }
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }
            return host;
        }
    }
}
