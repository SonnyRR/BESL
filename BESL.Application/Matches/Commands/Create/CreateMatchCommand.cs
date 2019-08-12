namespace BESL.Application.Matches.Commands.Create
{
    using System;
    using System.Collections.Generic;

    using MediatR;

    using BESL.Application.Common.Models.Lookups;

    public class CreateMatchCommand : IRequest<int>
    {
        public int PlayWeekId { get; set; }

        public int TournamentTableId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamId { get; set; }

        public DateTime PlayDate { get; set; }

        public IEnumerable<TeamsSelectItemLookupModel> Teams { get; set; }
    }
}
