namespace BESL.Application.Tournaments.Queries.Create
{
    using BESL.Application.Tournaments.Commands.Create;
    using MediatR;

    public class CreateTournamentQuery : IRequest<CreateTournamentCommand>
    {
    }
}
