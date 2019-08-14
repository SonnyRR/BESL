namespace BESL.Application.PlayWeeks.Queries
{
    using MediatR;

    public class GetPlayWeeksForTournamentTableQuery : IRequest<PlayWeeksForTournamentTableViewModel>
    {
        public int TournamentTableId { get; set; }
    }
}
