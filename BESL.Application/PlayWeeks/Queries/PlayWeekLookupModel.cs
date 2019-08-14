namespace BESL.Application.PlayWeeks.Queries
{
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class PlayWeekLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string WeekAsString { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlayWeek, PlayWeekLookupModel>()
                .ForMember(lm => lm.WeekAsString, o => o.MapFrom(src => $"{src.StartDate.ToString(DATE_FORMAT)} - {src.EndDate.ToString(DATE_FORMAT)}"));
        }
    }
}
