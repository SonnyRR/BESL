namespace BESL.Infrastructure.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using BESL.Application.Interfaces;

    public class UserNotificationHub : Hub, INotifyService
    {
        private readonly IUserAcessor userAcessor;
        private readonly IHubContext<UserNotificationHub> hubContext;

        public UserNotificationHub(IUserAcessor userAcessor, IHubContext<UserNotificationHub> hubContext)
        {
            this.userAcessor = userAcessor;
            this.hubContext = hubContext;
        }
         
        public async Task SendUserPushNotification(string header, string message, string type, string userId = null)
        {
            await this.hubContext
             .Clients
             .User(userId ?? this.userAcessor.UserId)
             .SendAsync("ReceivePushNotification", header, message, type);
        }
    }
}
