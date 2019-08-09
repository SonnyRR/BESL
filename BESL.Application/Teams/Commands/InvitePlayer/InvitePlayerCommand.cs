namespace BESL.Application.Teams.Commands.InvitePlayer
{
    using MediatR;

    public class InvitePlayerCommand : IRequest<int>
    {
        public int TeamId { get; set; }

        public string UserName { get; set; }

        public string SenderUsername { get; set; }
    }
}
