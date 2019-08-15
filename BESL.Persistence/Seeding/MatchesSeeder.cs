namespace BESL.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class MatchesSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            var weeks = await databaseContext
               .PlayWeeks
               .Where(x => x.TournamentTableId == 1)
               .ToListAsync();

            var random = new Random();

            foreach (var week in weeks)
            {
                for (int i = 1; i < 6; i += 2)
                {
                    var homeTeamScore = random.Next(0, 7);
                    var awayTeamScore = random.Next(0, 7);
                    var weekDayCounter = random.Next(1, 7);
                    var scheduledHour = new TimeSpan(random.Next(17, 23), random.Next(0, 61), 0).TotalHours;

                    var homeTeam = databaseContext.Teams.Include(x => x.PlayerTeams).SingleOrDefault(x => x.Id == i);
                    var awayTeam = databaseContext.Teams.Include(x => x.PlayerTeams).SingleOrDefault(x => x.Id == i + 1);

                    var match = new Match
                    {
                        HomeTeamId = homeTeam.Id,
                        AwayTeamId = awayTeam.Id,
                        HomeTeamScore = homeTeamScore,
                        AwayTeamScore = awayTeamScore,
                        PlayWeekId = week.Id,
                        ScheduledDate = week.StartDate.AddDays(weekDayCounter).AddHours(scheduledHour),
                        WinnerTeamId = homeTeamScore > awayTeamScore ? homeTeam.Id : awayTeam.Id,
                        IsDraw = homeTeamScore == awayTeamScore,
                        IsResultConfirmed = true
                    };

                    var participatedPlayers = homeTeam.PlayerTeams.Concat(awayTeam.PlayerTeams)
                        .Select(x => x.Player)
                        .ToList();

                    foreach (var player in participatedPlayers)
                    {
                        match.ParticipatedPlayers.Add(new PlayerMatch { PlayerId = player.Id, Match = match });
                    }

                    databaseContext.Matches.Add(match);
                }
            }
        }
    }
}
