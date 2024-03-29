﻿namespace BESL.Application.Games.Queries.Details
{
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class CompetitionFormatLookupModel : IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentFormat, CompetitionFormatLookupModel>();
        }
    }
}
