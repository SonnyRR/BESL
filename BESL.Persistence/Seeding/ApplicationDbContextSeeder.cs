﻿namespace BESL.Persistence.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using BESL.Application.Interfaces;
    using System.Threading;

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
                              new MainSeeder()
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
