namespace BESL.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    using BESL.Domain.Entities;

    public class UserNotificationHub : Hub
    {
        private readonly UserManager<Player> userManager;

        public UserNotificationHub(UserManager<Player> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SendUserSuccessNotificationAsync(string messageHeader, string message)
        {
            var userId = this.userManager.GetUserId(this.Context.User);

            await this.Clients
                .User(userId)
                .SendAsync("ReceiveMessageSuccess", messageHeader, message);
        }

        public async Task SendUserFailiureNotificationAsync(string messageHeader, string message)
        {
            var userId = this.userManager.GetUserId(this.Context.User);

            await this.Clients
                .User(userId)
                .SendAsync("ReceiveMessageFailiure", messageHeader, message);
        }
    }
}
