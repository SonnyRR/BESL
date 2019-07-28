namespace BESL.Web.Infrastructure.Services
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using BESL.Web.Hubs;
    using Microsoft.AspNetCore.SignalR;

    public class NotifyService : INotifyService
    {
        private readonly IHubContext<UserNotificationHub> hubContext;
        private readonly ConcurrentDictionary<string, string> nonPersistentPushNotifications;

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
