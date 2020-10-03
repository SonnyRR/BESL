namespace BESL.Application.Matches.Commands.Modify
{
    using System;

    using MediatR;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class ModifyMatchCommand : IMapFrom<Match>, IRequest<int>
    {
        public int Id { get; set; }

        public int PlayWeekId { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public DateTime ScheduledDate { get; set; }
    }
}
