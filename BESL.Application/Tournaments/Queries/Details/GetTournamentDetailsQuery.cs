namespace BESL.Application.Tournaments.Queries.Details
{
    using MediatR;

    public class GetTournamentDetailsQuery : IRequest<GetTournamentDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
