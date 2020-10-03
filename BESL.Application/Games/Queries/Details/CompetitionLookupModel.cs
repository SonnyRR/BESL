namespace BESL.Application.Games.Queries.Details
{
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class CompetitionLookupModel : IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public CompetitionFormatLookupModel CompetitionFormat { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Tournament, CompetitionLookupModel>();
        }
    }
}
