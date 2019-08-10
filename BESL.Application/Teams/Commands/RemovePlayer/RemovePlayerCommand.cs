namespace BESL.Application.Teams.Commands.RemovePlayer
{
    using MediatR;

    public class RemovePlayerCommand : IRequest<int>
    {
        public string PlayerId { get; set; }

        public int TeamId { get; set; }
    }
}
