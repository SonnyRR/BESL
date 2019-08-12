using BESL.Application.Interfaces;
using BESL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BESL.Persistence.Seeding
{
    public class TournamentFormatsSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            var formats = new TournamentFormat[]
            {
                new TournamentFormat
                {
                    Name = "6v6",
                    Description = "6v6 is the most common form of competitive format played in Team Fortress 2, and is often viewed as the 'default' format. It is played with 6 players per team, with class limits and weapons bans. The weapon bans and maps vary depending on the specific league.",
                    TeamPlayersCount = 6,
                    TotalPlayersCount = 12,
                    GameId = 1,
                    CreatedOn = DateTime.UtcNow,
                },
                new TournamentFormat
                {
                    Name = "9v9",
                    Description = "Highlander, a form of 9v9, is the largest competitive Team Fortress format. Its similarities to average public servers makes it an ideal starting point for new players, and its requirement that each team has one of every class means no matter what you like to play, there's a spot on the team for you.",
                    TeamPlayersCount = 9,
                    TotalPlayersCount = 18,
                    GameId = 1,
                    CreatedOn = DateTime.UtcNow
                },
                new TournamentFormat
                {
                    Name = "5v5",
                    Description = "Unlike casual mode, competitive mode always pits two teams of 5 against each other in a 30 round match. The roundtime is 1 minute 55 seconds and the bomb timer is 40 seconds. It is not possible to switch sides during the game except at the halftime. After the first 15 rounds, the game reaches halftime and the two teams will switch sides.",
                    TeamPlayersCount = 5,
                    TotalPlayersCount = 10,
                    GameId = 2,
                    CreatedOn = DateTime.UtcNow
                },
            };

            await databaseContext.AddRangeAsync(formats);
        }
    }
}