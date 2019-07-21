namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TournamentTableLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        public IEnumerable<TeamTableResultLookupModel> TableResults { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentTable, TournamentTableLookupModel>()
                .ForMember(tt => tt.TableResults, o => o.MapFrom(tt => tt.TeamTableResults.OrderBy(ttr => ttr.IsDeleted)));
        }
    }
}
