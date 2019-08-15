namespace BESL.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class TeamsSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            for (int i = 0; i < 6; i++)
            {
                var members = await databaseContext
                    .Players
                    .Where(x => x.UserName != ADMIN_USERNAME)
                    .OrderBy(x => x.CreatedOn)
                    .Skip(i * 6)
                    .Take(6)
                    .ToListAsync();

                var currentTeam = new Team
                {
                    Name = $"FooTeam{i + 1}",
                    TournamentFormatId = 1,
                    HomepageUrl = "https://vidin-drinking-team.bg",
                    Description = $"Hello, we are FooTeam{i + 1}",
                    OwnerId = members[0].Id,
                    ImageUrl = STEAM_DEFAULT_AVATAR_FULL
                };

                foreach (var member in members)
                {
                    currentTeam.PlayerTeams.Add(new PlayerTeam { Team = currentTeam, PlayerId = member.Id });
                }

                currentTeam.TeamTableResults.Add(new TeamTableResult { Team = currentTeam, TournamentTableId = 1 });

                databaseContext.Teams.Add(currentTeam);
            }


            var demoTeamMembers = await databaseContext
                    .Players
                    .Where(x => x.UserName != ADMIN_USERNAME)
                    .OrderBy(x => x.CreatedOn)
                    .Take(7)
                    .ToListAsync();

            var demoTeam = new Team
            {
                Name = $"FooTeamHighlander",
                TournamentFormatId = 2,
                HomepageUrl = "https://vidin-drinking-team.bg",
                Description = $"Hello, we are FooTeamHighlander",
                OwnerId = demoTeamMembers[0].Id,
                ImageUrl = STEAM_DEFAULT_AVATAR_FULL
            };

            foreach (var member in demoTeamMembers)
            {
                demoTeam.PlayerTeams.Add(new PlayerTeam { Team = demoTeam, PlayerId = member.Id });
            }

            databaseContext.Teams.Add(demoTeam);

        }
    }
}
