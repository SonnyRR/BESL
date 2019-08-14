namespace BESL.Application.Matches.Queries.EditMatchResult
{
    using MediatR;

    public class EditMatchResultQuery : IRequest<EditMatchResultQueryViewModel>
    {
        public int Id { get; set; }
    }
}
