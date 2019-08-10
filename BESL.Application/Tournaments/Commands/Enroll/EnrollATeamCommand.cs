namespace BESL.Application.Tournaments.Commands.Enroll
{
    using System.Collections.Generic;

    using MediatR;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Tournaments.Queries.Enroll;

    public class EnrollATeamCommand : IRequest<int>
    {
        public string TournamentName { get; set; }

        public int TournamentId { get; set; }

        public int TeamId { get; set; }

        public int TableId { get; set; }

        public IEnumerable<TeamsSelectItemLookupModel> Teams { get; set; }

        public IEnumerable<TournamentTableSelectItemLookupModel> Tables { get; set; }
    }
}
