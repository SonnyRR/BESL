namespace BESL.Application.Tests.Infrastructure
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    using BESL.Entities;
    using BESL.Persistence;
    using static BESL.Common.GlobalConstants;
    using BESL.Infrastructure;

    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(DateTime.Now.ToString() + Guid.NewGuid().ToString())
                .ReplaceService<IModelCacheKeyFactory, CustomDynamicModelCacheKeyFactory>()
                .Options;

            var dbContext = new ApplicationDbContext(dbContextOptions, new MachineDateTime());

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
                new Player{ Id = "Foo5", UserName = "FooP5", Email = "Foo@bg.bg", PasswordHash = "asd"},
            });
            dbContext.SaveChanges();

            var firstPlayer = dbContext.Players.SingleOrDefault(x => x.UserName == "FooP1");
            firstPlayer.Claims.Add(new IdentityUserClaim<string> { ClaimType = STEAM_ID_64_CLAIM_TYPE, ClaimValue = "sampleId" });

            var fifthPlayer = dbContext.Players.SingleOrDefault(x => x.UserName == "FooP5");
            fifthPlayer.Claims.Add(new IdentityUserClaim<string> { ClaimType = STEAM_ID_64_CLAIM_TYPE, ClaimValue = "sampleId2" });

            dbContext.SaveChanges();

            dbContext.AddRange(new[]
            {
                new Game { Name = "SampleGame1", Description = "SampleDescription1", GameImageUrl = Guid.NewGuid().ToString() },
                new Game { Name = "SampleGame2", Description = "SampleDescription2", GameImageUrl = Guid.NewGuid().ToString() },
                new Game { Name = "SampleGame3", Description = "SampleDescription3", GameImageUrl = Guid.NewGuid().ToString(), IsDeleted = true },
            });
            dbContext.SaveChanges();


            dbContext.AddRange(new[]
            {
                new TournamentFormat
                {
                    Name = "6v6",
                    GameId = 1,
                    TeamPlayersCount = 6,
                    TotalPlayersCount = 12,
                    Description = "Test"
                },
                new TournamentFormat
                {
                    Name = "9v9",
                    GameId = 2,
                    TeamPlayersCount = 9,
                    TotalPlayersCount = 18,
                    Description = "Test",
                },
                new TournamentFormat
                {
                    Name = "Deleted",
                    GameId = 2,
                    TeamPlayersCount = 6,
                    TotalPlayersCount = 12,
                    Description = "Test",
                    IsDeleted = true
                }
            });
            dbContext.SaveChanges();

            DateTime activeTournamentStartDate = new DateTime(2019, 08, 12);
            DateTime activeTournamentEndDate = new DateTime(2019, 09, 08);

            dbContext.AddRange(new[]
            {
                new Tournament {
                    Name = "TestTournament1",
                    FormatId = 1,
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
                new Tournament { Name = "TestTournament2", FormatId = 1, Description = "Test", StartDate = new DateTime(2019, 08, 12), EndDate = new DateTime(2019, 09, 08), IsActive = true },
            });
            dbContext.SaveChanges();

            dbContext.AddRange(new[]
            {
                new Team { Name = "FooTeam1", OwnerId = "Foo1", ImageUrl = "http://foo.bar/1.jpg", TournamentFormatId = 1, },
                new Team { Name = "FooTeam2", OwnerId = "Foo2", ImageUrl = "http://foo.bar/1.jpg", TournamentFormatId = 1, },
                new Team { Name = "FooTeam3", OwnerId = "Foo3", ImageUrl = "http://foo.bar/1.jpg", TournamentFormatId = 1, IsDeleted = true },
                new Team { Name = "FooTeam4", OwnerId = "Foo4", ImageUrl = "http://foo.bar/1.jpg", TournamentFormatId = 1, },
                new Team { Name = "FooTeam5", OwnerId = "Foo5", ImageUrl = "http://foo.bar/1.jpg", TournamentFormatId = 1, },
            });
            dbContext.SaveChanges();

            dbContext.AddRange(new[]
            {
                new PlayerTeam{ TeamId = 1, PlayerId = "Foo1" },
                new PlayerTeam{ TeamId = 2, PlayerId = "Foo2" },
                new PlayerTeam{ TeamId = 4, PlayerId = "Foo4" }
            });
            dbContext.SaveChanges();


            dbContext.AddRange(new[] {
                new TeamTableResult { TournamentTableId = 1, TeamId = 1, },
                new TeamTableResult { TournamentTableId = 1, TeamId = 4, IsDropped = true },
                new TeamTableResult { TournamentTableId = 1, TeamId = 3, },
                new TeamTableResult { TournamentTableId = 1, TeamId = 5, },
            });
            dbContext.SaveChanges();

            dbContext.AddRange(new[] {
                new Match {  HomeTeamId = 1, AwayTeamId = 2, HomeTeamScore = 2, AwayTeamScore = 1, IsResultConfirmed = true, PlayWeekId = 1, ScheduledDate = new DateTime(2019, 08, 13), WinnerTeamId = 1 },
                new Match {  HomeTeamId = 1, AwayTeamId = 2, HomeTeamScore = 5, AwayTeamScore = 2, IsResultConfirmed = true, PlayWeekId = 1, ScheduledDate = new DateTime(2019, 08, 14), WinnerTeamId = 1 },
            });
            dbContext.SaveChanges();

            dbContext.AddRange(new[] {
                new PlayerMatch{ PlayerId = "Foo1", MatchId = 1},
                new PlayerMatch{ PlayerId = "Foo2", MatchId = 1},
                new PlayerMatch{ PlayerId = "Foo1", MatchId = 2},
                new PlayerMatch{ PlayerId = "Foo2", MatchId = 2},
            });
            dbContext.SaveChanges();

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
