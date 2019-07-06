using AutoMapper;
using BESL.Application.Interfaces.Mapping;
using BESL.Domain.Entities;

namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{    
    public class TournamentTableLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        public TeamTableResultLookupModel TableResult { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentTable, TournamentTableLookupModel>();
        }
    }
}
