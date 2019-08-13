namespace BESL.Application.Tournaments.Queries.Modify
{
    using MediatR;
    using BESL.Application.Tournaments.Commands.Modify;

    public class TournamentModifyQuery : IRequest<ModifyTournamentCommand>
    {
        public int Id { get; set; }
    }
}
