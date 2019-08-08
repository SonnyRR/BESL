using MediatR;

namespace BESL.Application.Notifications.Commands.Delete
{
    public class DeleteNotificationCommand : IRequest<int>
    {
        public string Id { get; set; }
    }
}
