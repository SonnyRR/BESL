namespace BESL.Application.Players.Commands.AcceptInvite
{
    using MediatR;

    public class AcceptInviteCommand : IRequest<int>
    {
        public string InviteId { get; set; }
    }
}
