namespace BESL.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    using BESL.Domain.Entities;
    using BESL.Application.Interfaces;
    using static BESL.Common.GlobalConstants;

    public class UserNotificationHub : Hub
    {
        private readonly IUserAcessor userAcessor;
        private readonly IRedisService<Notification> notificationRedisService;

        public UserNotificationHub(IUserAcessor userAcessor, IRedisService<Notification> notificationRedisService)
        {
            this.userAcessor = userAcessor;
            this.notificationRedisService = notificationRedisService;
        }

        public async Task SendUserSuccessNotificationAsync(string header, string message)
        {
            var userId = this.userAcessor.UserId;
            await this.Clients
                .User(userId)
                .SendAsync("ReceiveMessageSuccess", header, message);
        }

        public async Task SendUserFailiureNotificationAsync()
        {
            var userId = this.userAcessor.UserId;
            var kvp = await this.notificationRedisService.Get(userId);

            await this.Clients
                .User(userId)
                .SendAsync("ReceiveMessageFailiure", ERROR_OCCURED_MSG, kvp.Content);
        }
    }
}
