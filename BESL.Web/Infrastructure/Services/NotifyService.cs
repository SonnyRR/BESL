namespace BESL.Web.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using BESL.Web.Hubs;

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<UserNotificationHub> hubContext;

        public NotifyService(IHubContext<UserNotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendUserFailiureNotificationAsync(string messageHeader, string message, string userId)
        {
            await this.hubContext
                .Clients
                .User(userId).SendAsync("ReceiveMessageFailiure", messageHeader, message);
        }

        public async Task SendUserSuccessNotificationAsync(string messageHeader, string message, string userId)
        {
            await this.hubContext
                .Clients
                .User(userId).SendAsync("ReceiveMessageSuccess", messageHeader, message);
        }
    }
}
