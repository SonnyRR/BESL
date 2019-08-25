namespace BESL.Application.Teams.Queries.GetAllTeamsPaged
{
    using System;

    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TeamLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerUsername { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TournamentFormat { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Team, TeamLookupModel>()
                .ForMember(lm=>lm.OwnerUsername, o => o.MapFrom(src=>src.Owner.UserName))
                .ForMember(lm=> lm.TournamentFormat, o => o.MapFrom(src=>$"{src.TournamentFormat.Name} - {src.TournamentFormat.Game.Name}"));
        }
    }
}
