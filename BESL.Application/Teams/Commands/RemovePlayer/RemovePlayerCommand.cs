namespace BESL.Application.Teams.Commands.RemovePlayer
{
    using MediatR;

    public class RemovePlayerCommand : IRequest<int>
    {
        public string CurrentUserId { get; set; }

        public string PlayerId { get; set; }

        public int TeamId { get; set; }
    }
}
