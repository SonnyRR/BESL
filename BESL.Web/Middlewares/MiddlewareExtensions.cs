namespace BESL.Web.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandlerMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

        public static IApplicationBuilder UseNotificationHandlerMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<NotificationHandlerMiddleware>();
    }
}
