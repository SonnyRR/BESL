namespace BESL.Application.Matches.Queries.EditMatchResult
{
    using MediatR;
    using BESL.Application.Matches.Commands.EditMatchResult;

    public class EditMatchResultQuery : IRequest<EditMatchResultCommand>
    {
        public int Id { get; set; }
    }
}
