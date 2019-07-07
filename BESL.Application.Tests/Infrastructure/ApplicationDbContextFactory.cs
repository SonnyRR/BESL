namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using BESL.Persistence;
    using BESL.Domain.Entities;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(dbContextOptions);

            dbContext.Database.EnsureCreated();

            var adminRole = new IdentityRole("Administrator");
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
                new Game(){Name = "SampleGame1", Description = "SampleDescription1", CreatedOn = DateTime.UtcNow, GameImageUrl = Guid.NewGuid().ToString()},
                new Game(){Name = "SampleGame2", Description = "SampleDescription2", CreatedOn = DateTime.UtcNow, GameImageUrl = Guid.NewGuid().ToString()},
                new Game(){Name = "SampleGame3", Description = "SampleDescription3", CreatedOn = DateTime.UtcNow, GameImageUrl = Guid.NewGuid().ToString()},
            });
            dbContext.SaveChanges();

            return dbContext;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
