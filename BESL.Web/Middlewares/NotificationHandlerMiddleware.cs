namespace BESL.Web.Middlewares
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class NotificationHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IUserAcessor userAcessor;
        private readonly IRedisService<Notification> redisNotificationService;

        public NotificationHandlerMiddleware(
            RequestDelegate next,
            IUserAcessor userAcessor,
            IRedisService<Notification> redisNotificationService)
        {
            this.next = next;
            this.userAcessor = userAcessor;
            this.redisNotificationService = redisNotificationService;
        }

        public async Task InvokeAsync(HttpContext context, INotifyService notifyService)
        {
            await this.next(context);

            if (context.User.Identity.IsAuthenticated)
            {
                var notification = await this.redisNotificationService.Get(this.userAcessor.UserId);

                if (notification != null)
                {
                    // Artificial delay in order for the response to be fully sent to the client.
                    Thread.Sleep(200);                    
                    await notifyService.SendUserPushNotification(notification.Header, notification.Content, notification.Type.ToString());
                }
            }
        }
    }
}