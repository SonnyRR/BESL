namespace BESL.Application.TournamentTables.Queries.GetTournamentTables
{
    using MediatR;

    public class GetTournamentTablesQuery : IRequest<TournamentTablesViewModel>
    {
        public int TournamentId { get; set; }
    }
}
