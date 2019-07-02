namespace BESL.Web.Middlewares
{
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using BESL.Application.Exceptions;
    using BESL.Web.Services;
    using Microsoft.AspNetCore.Http;

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, INotifyService notifyService)
        {
            string userNameIdentifier = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool isCurrentUserAdmin = context.User.IsInRole("Administrator");

            if (isCurrentUserAdmin)
            {
                try
                {
                    await this.next(context);
                }
                catch (ValidationException validationException)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var kvp in validationException.Failures)
                    {
                        string propertyName = kvp.Key;
                        string[] propertyFailure = kvp.Value;

                        stringBuilder.AppendLine($"{propertyName}:");

                        foreach (var failure in propertyFailure)
                        {
                            stringBuilder.AppendLine($" *{failure}");
                        }
                    }
                    await notifyService.SendUserFailiureNotificationAsync(validationException.Message, stringBuilder.ToString(), userNameIdentifier);
                    await this.next(context);
                }
                catch (BaseCustomException exception)
                {
                    await notifyService.SendUserFailiureNotificationAsync("An error has occured!", exception.Message, userNameIdentifier);
                    await this.next(context);
                }
            }
            else
            {
                await this.next(context);
            }
        }
    }
}
