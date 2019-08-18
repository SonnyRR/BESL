using MediatR;

namespace BESL.Application.Notifications.Commands.Delete
{
    public class DeleteNotificationCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
