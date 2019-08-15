namespace BESL.Persistence.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Application.Interfaces;

    public class ApplicationDbContextSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (dbContext.Players.Any() || dbContext.Games.Any())
            {
                // DB is seeded.
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<IDbSeeder>
                          {
                              new RoleSeeder(),
                              new RootAdminSeeder(),
                              new UsersSeeder(),
                              new GamesSeeder(),
                              new TournamentFormatsSeeder(),
                              new TournamentsSeeder(),
                              new TeamsSeeder(),
                              new MatchesSeeder()
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync(CancellationToken.None);
                logger.LogInformation($"Seeder {seeder.GetType().Name} - COMPLETED.");
            }
        }
    }
}
