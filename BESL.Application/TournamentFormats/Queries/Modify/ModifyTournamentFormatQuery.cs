namespace BESL.Application.TournamentFormats.Queries.Modify
{
    using MediatR;
    using BESL.Application.TournamentFormats.Commands.Modify;

    public class ModifyTournamentFormatQuery : IRequest<ModifyTournamentFormatCommand>
    {
        public int Id { get; set; }
    }
}
