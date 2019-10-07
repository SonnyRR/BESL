namespace BESL.Application.Notifications.Commands.Delete
{
    using MediatR;
    
    public class DeleteNotificationCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
