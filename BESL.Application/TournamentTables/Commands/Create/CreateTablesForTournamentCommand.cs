namespace BESL.Application.TournamentTables.Commands.Create
{
    using MediatR;

    public class CreateTablesForTournamentCommand : IRequest<int>
    {
        public int TournamentId { get; set; }
    }
}
