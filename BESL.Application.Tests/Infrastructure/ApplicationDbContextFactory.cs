namespace BESL.Application.Tests.Infrastructure
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using BESL.Persistence;
    using BESL.Domain.Entities;
    using Microsoft.Extensions.DependencyInjection;
    using System.ComponentModel;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using System.Threading.Tasks;

    public class ApplicationDbContextFactory
    {

        public static async Task<ApplicationDbContext> Create()
        {

            //var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString())
            //    .Options;

            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),
                ServiceLifetime.Transient);
            var provider = services.BuildServiceProvider();

            var dbContext = provider.GetService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();

            var adminRole = new PlayerRole("Administrator");
            dbContext.Roles.Add(adminRole);
            dbContext.SaveChanges();

            dbContext.Players.AddRange(new[] {
                new Player
                {
                    Id = "PlayerOne",
                    UserName = "VasilKotsev",
                    Email = "vidin.drinking.team@abv.bg",
                    CreatedOn = new DateTime(2019, 07, 03),
                }
            });
            dbContext.SaveChanges();

            dbContext.UserRoles
                .Add(new IdentityUserRole<string>() { RoleId = adminRole.Id, UserId = "PlayerOne" });

            dbContext.SaveChanges();

            dbContext.Games.AddRange(new[]
            {
                new Game(){ Name = "SampleGame1", Description = "SampleDescription1", CreatedOn = DateTime.UtcNow, GameImageUrl = Guid.NewGuid().ToString()},
                new Game(){ Name = "SampleGame3", Description = "SampleDescription3", CreatedOn = DateTime.UtcNow, GameImageUrl = Guid.NewGuid().ToString()},
                new Game(){ Name = "SampleGame2", Description = "SampleDescription2", CreatedOn = DateTime.UtcNow, GameImageUrl = Guid.NewGuid().ToString()},
            });

            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Entries<Game>().ToList().ForEach(x => x.State = EntityState.Detached);
            return dbContext;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context?.Database.EnsureDeleted();
            context?.Dispose();
        }
    }
}
