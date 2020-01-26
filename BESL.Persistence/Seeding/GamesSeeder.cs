namespace BESL.Persistence.Seeding
{

    using System;
    using System.Threading.Tasks;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GamesSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            var games = new Game[]
            {
                new Game
                {
                    Name = "Team Fortress 2",
                    Description = "One of the most popular online action games of all time, Team Fortress 2 delivers constant free updates—new game modes, maps, equipment and, most importantly, hats. Nine distinct classes provide a broad range of tactical abilities and personalities, and lend themselves to a variety of player skills. New to TF? Don’t sweat it! No matter what your style and experience, we’ve got a character for you. Detailed training and offline practice modes will help you hone your skills before jumping into one of TF2’s many game modes, including Capture the Flag, Control Point, Payload, Arena, King of the Hill and more. Make a character your own! There are hundreds of weapons, hats and more to collect, craft, buy and trade. Tweak your favorite class to suit your gameplay style and personal taste. You don’t need to pay to win—virtually all of the items in the Mann Co. Store can also be found in-game.",
                    GameImageUrl = "https://steamcdn-a.akamaihd.net/steam/apps/440/header.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new Game
                {
                    Name = "Counter-Strike: Global Offensive",
                    Description = @"Counter-Strike: Global Offensive (CS: GO) expands upon the team-based action gameplay that it pioneered when it was launched 19 years ago. CS: GO features new maps, characters, weapons, and game modes, and delivers updated versions of the classic CS content. ""Counter-Strike took the gaming industry by surprise when the unlikely MOD became the most played online PC action game in the world almost immediately after its release in August 1999,"" said Doug Lombardi at Valve. ""For the past 12 years, it has continued to be one of the most-played games in the world, headline competitive gaming tournaments and selling over 25 million units worldwide across the franchise. CS: GO promises to expand on CS' award-winning gameplay and deliver it to gamers on the PC as well as the next gen consoles and the Mac.""",
                    GameImageUrl = "https://steamcdn-a.akamaihd.net/steam/apps/730/header.jpg",
                    CreatedOn = DateTime.UtcNow
                }
            };

            await databaseContext.AddRangeAsync(games);
        }
    }
}
