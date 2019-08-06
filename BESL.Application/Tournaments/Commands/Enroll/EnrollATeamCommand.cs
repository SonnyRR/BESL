namespace BESL.Application.Tournaments.Commands.Enroll
{
    using System.Collections.Generic;
    using BESL.Application.Common.Models.Lookups;

    public class EnrollATeamCommand
    {
        public string UserId { get; set; }

        public int TournamentId { get; set; }

        public IEnumerable<TeamsSelectItemLookupModel> Teams { get; set; }
    }
}
