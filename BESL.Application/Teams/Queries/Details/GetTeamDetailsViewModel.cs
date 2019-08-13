namespace BESL.Application.Teams.Queries.Details
{
    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces.Mapping;

    public class GetTeamDetailsViewModel : IMapFrom<TeamDetailsLookupModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string TeamImageUrl { get; set; }

        public string CreatedOn { get; set; }

        public bool IsOwner { get; set; }

        public bool IsMember { get; set; }

        public string TournamentFormat { get; set; }
    }
}
