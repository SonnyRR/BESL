namespace BESL.Application.Players.Commands.DeclineInvite
{
    using MediatR;

    public class DeclineInviteCommand : IRequest<int>
    {
        public string InviteId { get; set; }
    }
}
