namespace BESL.Web.Infrastructure.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    using BESL.Application.Interfaces;
    using BESL.Web.Hubs;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class NotifyService : INotifyService
    {
        private readonly IUserAcessor userAcessor;
        private readonly IRedisService<Notification> notificationRedisService;
        private readonly IHubContext<UserNotificationHub> hubContext;

        public NotifyService(IUserAcessor userAcessor, 
            IRedisService<Notification> notificationRedisService, 
            IHubContext<UserNotificationHub> hubContext)
        {
            this.userAcessor = userAcessor;
            this.notificationRedisService = notificationRedisService;
            this.hubContext = hubContext;
        }

        public async Task SendUserSuccessNotificationAsync(string header, string message)
        {
            var userId = this.userAcessor.UserId;
            await this.hubContext
                .Clients
                .User(userId)
                .SendAsync("ReceiveMessageSuccess", header, message);
        }

        public async Task SendUserFailiureNotificationAsync()
        {
            var userId = this.userAcessor.UserId;
            var kvp = await this.notificationRedisService.Get(userId);

            await this.hubContext
                .Clients
                .User(userId)
                .SendAsync("ReceiveMessageFailiure", ERROR_OCCURED_MSG, kvp.Content);
        }
    }
}
