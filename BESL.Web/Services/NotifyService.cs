namespace BESL.Web.Services
{
    using BESL.Web.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<UserNotificationHub> hubContext;

        public NotifyService(IHubContext<UserNotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendUserFailiureNotificationAsync(string message, string userId)
        {
            await this.hubContext.Clients.User(userId).SendAsync("ReceiveMessageFailiure", message);
        }

        public async Task SendUserSuccessNotificationAsync(string message, string userId)
        {
            await this.hubContext.Clients.User(userId).SendAsync("ReceiveMessageSuccess", message);
        }
    }
}
