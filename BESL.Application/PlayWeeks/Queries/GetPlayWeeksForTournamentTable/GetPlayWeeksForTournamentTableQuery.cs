namespace BESL.Application.PlayWeeks.Queries.GetPlayWeeksForTournamentTable
{
    using MediatR;

    public class GetPlayWeeksForTournamentTableQuery : IRequest<PlayWeeksForTournamentTableViewModel>
    {
        public int TournamentTableId { get; set; }
    }
}
