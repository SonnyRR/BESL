namespace BESL.Application.Notifications.Queries.GetNotificationsForPlayer
{
    using MediatR;

    public class GetNotificationsForPlayerQuery : IRequest<PlayerNotificationsViewModel>
    {
        public string UserId { get; set; }
    }
}
