namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TournamentTableLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        public PlayWeekLookupModel CurrentPlayWeek { get; set; }

        public IEnumerable<TeamTableResultLookupModel> TableResults { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentTable, TournamentTableLookupModel>()
                .ForMember(lm => lm.TableResults, o => o.MapFrom(tt => tt.TeamTableResults.OrderBy(ttr => ttr.IsDeleted)))
                .ForMember(lm => lm.CurrentPlayWeek, o => o.MapFrom(tt => tt.PlayWeeks.SingleOrDefault(pw => pw.IsActive)));
        }
    }
}
