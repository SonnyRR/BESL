namespace BESL.Web.Infrastructure.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class UserNotificationHub : Hub
    {
        public async Task SendUserSuccessNotificationAsync(string messageHeader, string message, string userId)
        {
            await this.Clients
                .User(userId)
                .SendAsync("ReceiveMessageSuccess", messageHeader, message);
        }

        public async Task SendUserFailiureNotificationAsync(string messageHeader, string message, string userId)
        {
            await this.Clients
                .User(userId)
                .SendAsync("ReceiveMessageFailiure", messageHeader, message);
        }
    }
}
