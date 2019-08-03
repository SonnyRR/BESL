namespace BESL.Application.Teams.Queries.Details
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class GetTeamDetailsViewModel : IMapFrom<Team>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string TeamImageUrl { get; set; }

        public string CreatedOn { get; set; }
    }
}
