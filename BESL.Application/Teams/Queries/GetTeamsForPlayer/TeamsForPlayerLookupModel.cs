﻿namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    public class TeamForPlayerLookupModel : IHaveCustomMapping
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string JoinedDate { get; set; }

        public string LeftDate { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlayerTeam, TeamForPlayerLookupModel>()
                .ForMember(lm => lm.JoinedDate, o => o.MapFrom(pt => pt.CreatedOn.ToString(DATE_FORMAT)))
                .ForMember(lm => lm.LeftDate, o => o.MapFrom(pt => pt.DeletedOn.HasValue ? pt.DeletedOn.Value.ToString(DATE_FORMAT) : "N/A"));
        }
    }
}
