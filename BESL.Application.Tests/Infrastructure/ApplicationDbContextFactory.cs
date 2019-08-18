namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    using BESL.Domain.Entities;
    using BESL.Persistence;
    using static BESL.Common.GlobalConstants;

    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(DateTime.Now.ToString() + Guid.NewGuid().ToString())
                .ReplaceService<IModelCacheKeyFactory, CustomDynamicModelCacheKeyFactory>() // maika mu deaba na toq caching
                .Options;

            var dbContext = new ApplicationDbContext(dbContextOptions);

            //ServiceCollection services = new ServiceCollection();
            //services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),
            //    ServiceLifetime.Transient);
            //var provider = services.BuildServiceProvider();

            //var dbContext = provider.GetService<ApplicationDbContext>();
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

            dbContext.AddRange(new[]
            {
                new Player{ Id = "Foo1", UserName = "FooP1", Email = "Foo@bg.bg", PasswordHash = "asd"},
                new Player{ Id = "Foo2", UserName = "FooP2", Email = "Foo@bg.bg", PasswordHash = "asd"},
                new Player{ Id = "Foo3", UserName = "FooP3", Email = "Foo@bg.bg", PasswordHash = "asd"},
                new Player{ Id = "Foo4", UserName = "FooP4", Email = "Foo@bg.bg", PasswordHash = "asd"},
            });
            dbContext.SaveChanges();

            dbContext.AddRange(new[]
            {
                new Game { Id = 2, Name = "SampleGame1", Description = "SampleDescription1", GameImageUrl = Guid.NewGuid().ToString() },
                new Game { Id = 3, Name = "SampleGame2", Description = "SampleDescription2", GameImageUrl = Guid.NewGuid().ToString() },
                new Game { Id = 4, Name = "SampleGame3", Description = "SampleDescription3", GameImageUrl = Guid.NewGuid().ToString(), IsDeleted = true },
            });
            dbContext.SaveChanges();


            dbContext.AddRange(new[]
            {
                new TournamentFormat { Id = 2, Name = "6v6", GameId = 2, TeamPlayersCount = 6 , TotalPlayersCount = 12,  Description = "Test" },
                new TournamentFormat { Id = 3, Name = "Deleted", GameId = 2, TeamPlayersCount = 6 , TotalPlayersCount = 12,  Description = "Test", IsDeleted = true }
            });
            dbContext.SaveChanges();

            DateTime activeTournamentStartDate = new DateTime(2019, 08, 12);
            DateTime activeTournamentEndDate = new DateTime(2019, 09, 08);

            dbContext.AddRange(new[]
            {
                new Tournament {
                    Id = 2,
                    Name = "TestTournament1",
                    FormatId = 2,
                    Description = "Test",
                    StartDate = activeTournamentStartDate,
                    EndDate = activeTournamentEndDate,
                    IsActive = true,
                    Tables = new List<TournamentTable>()
                    {
                        new TournamentTable
                        {
                            Name = OPEN_TABLE_NAME,
                            MaxNumberOfTeams = OPEN_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(-1), IsActive = false},
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(6), IsActive = false},
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(13), IsActive = true},
                            }
                        },
                        new TournamentTable
                        {
                            Name = MID_TABLE_NAME,
                            MaxNumberOfTeams = MID_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(-1), IsActive = false},
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(6), IsActive = false},
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(13), IsActive = true},
                            }
                        },
                        new TournamentTable
                        {
                            Name = PREM_TABLE_NAME,
                            MaxNumberOfTeams = PREM_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(-1), IsActive = false},
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(6), IsActive = false},
                                new PlayWeek { StartDate = activeTournamentStartDate.AddDays(13), IsActive = true},
                            }
                        },
                    },
                },
                new Tournament { Id = 3, Name = "TestTournament2", FormatId = 2, Description = "Test", StartDate = new DateTime(2019, 08, 12), EndDate = new DateTime(2019, 09, 08), IsActive = true },
            });
            dbContext.SaveChanges();

            dbContext.AddRange(new[]
            {
                new Team { Id = 2, Name = "FooTeam1", OwnerId = "Foo1", ImageUrl = "http://foo.bar/1.jpg" },
                new Team { Id = 3, Name = "FooTeam2", OwnerId = "Foo2", ImageUrl = "http://foo.bar/1.jpg" },
                new Team { Id = 4, Name = "FooTeam3", OwnerId = "Foo3", ImageUrl = "http://foo.bar/1.jpg", IsDeleted = true },
            });

            DetachAllEntities(dbContext);
            return dbContext;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void DetachAllEntities(ApplicationDbContext context)
        {
            var changedEntriesCopy = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted ||
                            e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
