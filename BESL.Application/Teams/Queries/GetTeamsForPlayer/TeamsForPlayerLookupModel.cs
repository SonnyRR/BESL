namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TeamForPlayerLookupModel : IHaveCustomMapping
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string JoinedDate { get; set; }

        public string LeftDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlayerTeam, TeamForPlayerLookupModel>()
                .ForMember(lm => lm.JoinedDate, o => o.MapFrom(pt => pt.CreatedOn.ToString()))
                .ForMember(lm => lm.LeftDate, o => o.MapFrom(pt => pt.DeletedOn.HasValue ? pt.DeletedOn.Value.ToString() : "N/A"));
        }
    }
}
