namespace BESL.Application.PlayWeeks.Queries.GetPlayWeeksForTournamentTable
{
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    public class PlayWeekLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string WeekAsString { get; set; }

        public string TournamentTableName { get; set; }

        public string TournamentName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlayWeek, PlayWeekLookupModel>()
                .ForMember(lm => lm.WeekAsString, o => o.MapFrom(src => $"{src.StartDate.ToString(DATE_FORMAT)} - {src.EndDate.ToString(DATE_FORMAT)}"))
                .ForMember(lm => lm.TournamentTableName, o => o.MapFrom(src => src.TournamentTable.Name))
                .ForMember(lm => lm.TournamentName, o => o.MapFrom(src => src.TournamentTable.Tournament.Name));
        }
    }
}
