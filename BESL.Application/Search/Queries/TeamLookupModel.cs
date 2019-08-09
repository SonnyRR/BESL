namespace BESL.Application.Search.Queries
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TeamLookupModel : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
