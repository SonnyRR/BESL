namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TeamTableResultLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public string Team { get; set; }

        public bool IsDropped { get; set; }

        public int TournamentTableId { get; set; }

        public int MatchesPlayed { get; set; }

        public int MatchesWon { get; set; }

        public int MatchesLost { get; set; }

        public int TotalPoints { get; set; }

        public int PenaltyPoints { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TeamTableResult, TeamTableResultLookupModel>()
                .ForMember(lm => lm.Team, o => o.MapFrom(ttr => ttr.Team.Name))
                .ForMember(lm => lm.IsDropped, o => o.MapFrom(ttr => ttr.IsDeleted));
        }
    }
}
