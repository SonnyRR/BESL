namespace BESL.Application.Tournaments.Queries.Modify
{
    using BESL.Application.Tournaments.Commands.Modify;
    using MediatR;

    public class TournamentModifyQuery : IRequest<ModifyTournamentCommand>
    {
        public int Id { get; set; }
    }
}
