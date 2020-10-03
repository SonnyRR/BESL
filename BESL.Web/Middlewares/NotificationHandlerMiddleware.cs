namespace BESL.Web.Middlewares
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces;
    using BESL.Entities;

    public class NotificationHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IRedisService<Notification> redisNotificationService;

        public NotificationHandlerMiddleware(
            RequestDelegate next,  
            IRedisService<Notification> redisNotificationService)
        {
            this.next = next;
            this.redisNotificationService = redisNotificationService;
        }

        public async Task InvokeAsync(HttpContext context, IUserAccessor userAccessor, INotifyService notifyService)
        {
            await this.next(context);

            if (context.User.Identity.IsAuthenticated && context.Response.StatusCode == StatusCodes.Status204NoContent)
            {
                var notification = await this.redisNotificationService.Get(userAccessor.UserId);

                if (notification != null)
                {
                    await notifyService.SendUserPushNotification(notification.Header, notification.Content, notification.Type.ToString());
                }
            }
        }
    }
}