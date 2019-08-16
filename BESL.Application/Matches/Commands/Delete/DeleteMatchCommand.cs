namespace BESL.Application.Matches.Commands.Delete
{
    using MediatR;

    public class DeleteMatchCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
