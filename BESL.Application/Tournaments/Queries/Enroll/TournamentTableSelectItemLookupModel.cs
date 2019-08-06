namespace BESL.Application.Tournaments.Queries.Enroll
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TournamentTableSelectItemLookupModel : IMapFrom<TournamentTable>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
