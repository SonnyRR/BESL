namespace BESL.Application.Matches.Queries.GetMatchesForPlayWeek
{
    using MediatR;

    public class GetMatchesForPlayWeekQuery : IRequest<MatchesForPlayWeekViewModel>
    {
        public int PlayWeekId { get; set; }
    }
}
