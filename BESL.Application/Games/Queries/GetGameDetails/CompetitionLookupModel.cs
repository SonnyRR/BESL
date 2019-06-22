using System;
using AutoMapper;
using BESL.Application.Games.Common;
using BESL.Application.Interfaces.Mapping;
using BESL.Domain.Entities;

namespace BESL.Application.Games.Queries.GetGameDetails
{
    public class  CompetitionLookupModel : IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public CompetitionFormatLookupModel CompetitionFormat { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Competition, CompetitionLookupModel>();
        }
    }
}
