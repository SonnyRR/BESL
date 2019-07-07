namespace BESL.Application.TournamentFormats.Queries.Modify
{
    using MediatR;

    public class ModifyTournamentFormatQuery : IRequest<ModifyTournamentFormatViewModel>
    {
        public int Id { get; set; }
    }
}
