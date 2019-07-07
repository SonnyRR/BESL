namespace BESL.Persistence
{
    using System;
    using System.Linq;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Games.Any() && dbContext.Users.Any())
            {
                return; // Database is seeded.
            }

            var administratorUser = dbContext.Users.SingleOrDefault(u => u.UserName == ADMIN_USERNAME);

            if (administratorUser == null)
            {
                throw new InvalidOperationException("Root administrator account is not present!");
            }

            #region Games
            var games = new Game[]
            {
                new Game() { Name = "Team Fortress 2", Description = "One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills. New to TF? Don’t sweat it! No matter what your style and experience, we’ve got a character for you. Detailed training and offline practice modes will help you hone your skills before jumping into one of TF2’s many game modes, including Capture the Flag, Control Point, Payload, Arena, King of the Hill and more. Make a character your own! There are hundreds of weapons, hats and more to collect, craft, buy and trade. Tweak your favorite class to suit your gameplay style and personal taste. You don’t need to pay to win—virtually all of the items in the Mann Co. Store can also be found in-game.", GameImageUrl = "https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg?t=1558031605", CreatedOn = DateTime.UtcNow }
            };

            foreach (var g in games)
            {
                dbContext.Games.Add(g);
            }
            dbContext.SaveChanges();
            #endregion

            #region TournamentFormats
            var formats = new TournamentFormat[]
            {
                new TournamentFormat() { Name = "6v6", Description = "6v6 is the most common form of competitive format played in Team Fortress 2, and is often viewed as the 'default' format. It is played with 6 players per team, with class limits and weapons bans. The weapon bans and maps vary depending on the specific league.", TeamPlayersCount = 6, TotalPlayersCount=12, GameId = 1, CreatedOn = DateTime.UtcNow },
                new TournamentFormat() { Name = "9v9", Description = "Highlander, a form of 9v9, is the largest competitive Team Fortress format. Its similarities to average public servers makes it an ideal starting point for new players, and its requirement that each team has one of every class means no matter what you like to play, there's a spot on the team for you.", TeamPlayersCount = 9, TotalPlayersCount=18, GameId = 1, CreatedOn = DateTime.UtcNow },
            };

            foreach (var tf in formats)
            {
                dbContext.TournamentFormats.Add(tf);
            }
            dbContext.SaveChanges();
            #endregion

            #region Tournaments
            var tournaments = new Tournament[]
            {
                new Tournament(){ Name = "ThermalTake eSports 2019 6v6", Description = "Play to win prizes by our sponsor Thermaltake including: keyboards, mices, gaming gear and PC components", GameId =1, CreatedOn = DateTime.UtcNow, FormatId = 1 },
                new Tournament(){ Name = "Corsair 9v9 Summer Highlander", Description = "Corsair sponsors this round of summer 9v9 madness!. Prize pool includes a one-off custom gaming PC and many peripherals!", GameId = 1, CreatedOn = DateTime.UtcNow, FormatId = 2}
            };
            foreach (var t in tournaments)
            {
                dbContext.Tournaments.Add(t);
            }
            dbContext.SaveChanges();
            #endregion

            dbContext.SaveChanges();
        }
    }
}
