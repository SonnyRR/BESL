namespace BESL.Infrastructure.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using BESL.Application.Interfaces;

    public class UserNotificationHub : Hub, INotifyService
    {
        private readonly IUserAcessor userAcessor;

        public UserNotificationHub(IUserAcessor userAcessor)
        {
            this.userAcessor = userAcessor;
        }

        public async Task SendUserSuccessNotificationAsync(string header, string message, string userId = null)
        {
            await this.Clients
                .User(userId ?? this.userAcessor.UserId)
                .SendAsync("ReceiveMessageSuccess", header, message);
        }

        public async Task SendUserFailiureNotificationAsync(string header, string message, string userId = null)
        {
            await this.Clients
                .User(userId ?? this.userAcessor.UserId)
                .SendAsync("ReceiveMessageFailiure", header, message);
        }
    }
}
