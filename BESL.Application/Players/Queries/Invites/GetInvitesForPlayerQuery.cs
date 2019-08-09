namespace BESL.Application.Players.Queries.Invites
{
    using MediatR;

    public class GetInvitesForPlayerQuery : IRequest<InvitesViewModel>
    {
        public string UserId { get; set; }
    }
}
