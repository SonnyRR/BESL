namespace BESL.Infrastructure.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using BESL.Application.Interfaces;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;

    public class UserNotificationHub : Hub, INotifyService
    {
        private readonly IUserAccessor userAccessor;
        private readonly IHubContext<UserNotificationHub> hubContext;

        public UserNotificationHub(IUserAccessor userAccessor, IHubContext<UserNotificationHub> hubContext)
        {
            this.userAccessor = userAccessor;
            this.hubContext = hubContext;
        }
         
        public async Task SendUserPushNotification(string header, string message, string type, string userId = null)
        {
            await this.hubContext
             .Clients
             .User(userId ?? this.userAccessor.UserId)
             .SendAsync("ReceivePushNotification", header, message, type);
        }

        public Task Test()
        {

            var uri = new Uri("/userNotificationHub", UriKind.Absolute);
            var signalRConnection = new HubConnectionBuilder()
                 .WithUrl(uri)
                 .Build();

            return signalRConnection.StartAsync();
        }
    }
}
