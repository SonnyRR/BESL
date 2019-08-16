namespace BESL.Application.Matches.Commands.AcceptResult
{
    using MediatR;

    public class AcceptMatchResultCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
