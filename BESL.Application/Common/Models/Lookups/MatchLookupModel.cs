namespace BESL.Application.Common.Models.Lookups
{
    using System;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class MatchLookupModel : IMapFrom<Match>
    {
        public int Id { get; set; }

        public int PlayWeekId { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public int WinnerTeamId { get; set; }

        public string WinnerTeamName { get; set; }

        public bool IsDraw { get; set; }

        public DateTime ScheduledDate { get; set; }
    }
}
