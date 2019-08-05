namespace BESL.Application.Tournaments.Commands.Delete
{
    using MediatR;

    public class DeleteTournamentCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
