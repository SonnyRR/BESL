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
        private readonly IRedisService<Notification> redisNotificationService;

        public NotificationHandlerMiddleware(
            RequestDelegate next,  
            IRedisService<Notification> redisNotificationService)
        {
            this.next = next;
            this.redisNotificationService = redisNotificationService;
        }

        public async Task InvokeAsync(HttpContext context, IUserAcessor userAcessor, INotifyService notifyService)
        {
            await this.next(context);

            if (context.User.Identity.IsAuthenticated && context.Response.StatusCode == StatusCodes.Status204NoContent)
            {
                var notification = await this.redisNotificationService.Get(userAcessor.UserId);

                if (notification != null)
                {
                    // Artificial delay in order for the response to be fully sent to the client.
                    Thread.Sleep(200);                    
                    await notifyService.SendUserPushNotification(notification.Header, notification.Content, notification.Type.ToString());
                    await this.redisNotificationService.Delete(userAcessor.UserId);
                }
            }
        }
    }
}