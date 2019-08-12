namespace BESL.Application.TournamentTables.Queries.GetTeamsForTournamentTable
{
    using MediatR;

    public class GetTeamsForTournamentTableQuery : IRequest<TeamsForTournamentTableViewModel>
    {
        public int TournamentTableId { get; set; }
    }
}
