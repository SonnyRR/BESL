namespace BESL.Persistence.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class TournamentsSeeder : IDbSeeder
    {
        public async Task SeedAsync(IApplicationDbContext databaseContext, IServiceProvider serviceProvider)
        {
            DateTime startDate = new DateTime(2019, 08, 12);
            DateTime endDate = new DateTime(2019, 09, 08);

            var tournaments = new Tournament[]
            {
                new Tournament
                {
                    Name = "ThermalTake eSports 2019 6v6",
                    Description = "Play to win prizes by our sponsor Thermaltake including: keyboards, mices, gaming gear and PC components",
                    FormatId = 1,
                    TournamentImageUrl = "https://res.cloudinary.com/vasil-kotsev/image/upload/c_scale,h_215,w_460/v1564073951/BESL/logo-tt-esports_xpuspv.jpg",
                    AreSignupsOpen = true,
                    IsActive = false,
                    Tables = new List<TournamentTable>()
                    {
                        new TournamentTable
                        {
                            Name = OPEN_TABLE_NAME,
                            MaxNumberOfTeams = OPEN_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = startDate.AddDays(-1), IsActive = true}
                            }
                        },
                        new TournamentTable
                        {
                            Name = MID_TABLE_NAME,
                            MaxNumberOfTeams = MID_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = startDate.AddDays(-1), IsActive = true }
                            }
                        },
                        new TournamentTable
                        {
                            Name = PREM_TABLE_NAME,
                            MaxNumberOfTeams = PREM_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = startDate.AddDays(-1), IsActive = true }
                            }
                        },
                    },
                    StartDate = startDate,
                    EndDate = endDate
                },

                new Tournament
                {
                    Name = "Corsair 9v9 Summer Highlander",
                    Description = "Corsair sponsors this round of summer 9v9 madness!. Prize pool includes a one-off custom gaming PC and many peripherals!",
                    FormatId = 2,
                    TournamentImageUrl = "https://res.cloudinary.com/vasil-kotsev/image/upload/c_scale,h_215,w_460/v1564074113/BESL/blog_Introducing-The-New-Sails-Logo-Content-1_vbr56j.png",
                    AreSignupsOpen = true,
                    IsActive = false,
                    Tables = new List<TournamentTable>()
                    {
                        new TournamentTable
                        {
                            Name = OPEN_TABLE_NAME,
                            MaxNumberOfTeams = OPEN_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = startDate.AddDays(-1), IsActive = true }
                            }
                        },
                        new TournamentTable
                        {
                            Name = MID_TABLE_NAME,
                            MaxNumberOfTeams = MID_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = startDate.AddDays(-1), IsActive = true }
                            }
                        },
                        new TournamentTable
                        {
                            Name = PREM_TABLE_NAME,
                            MaxNumberOfTeams = PREM_TABLE_MAX_TEAMS,
                            PlayWeeks = new HashSet<PlayWeek>
                            {
                                new PlayWeek { StartDate = startDate.AddDays(-1), IsActive = true }
                            }
                        },
                    },
                    StartDate = startDate,
                    EndDate = endDate
                }
            };

            await databaseContext.AddRangeAsync(tournaments);
        }
    }
}
