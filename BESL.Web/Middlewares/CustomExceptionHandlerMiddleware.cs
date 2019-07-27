namespace BESL.Web.Middlewares
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Threading;

    using Microsoft.AspNetCore.Http;

    using BESL.Application.Exceptions;
    using BESL.Domain.Entities.Enums;
    using BESL.Web.Infrastructure.Services;
    using static BESL.Common.GlobalConstants;
    using System.Net;

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, INotifyService notifyService)
        {
            string userNameIdentifier = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isCurrentUserAdmin = context.User.IsInRole(Role.Administrator.ToString());

            if (userNameIdentifier != null)
            {
                try
                {
                    await this.next(context);
                }
                catch (ValidationException validationException)
                {
                    await notifyService.SendUserFailiureNotificationAsync(ERROR_OCCURED_MSG, validationException.Message, userNameIdentifier);
                    await this.next(context);
                }
                catch (BaseCustomException exception)
                {
                    if (context.Request.Method == HttpMethod.Post.Method)
                    {
                        context.Request.Method = HttpMethod.Get.Method;
                        await this.next(context);
                        Thread.Sleep(100);
                        await notifyService.SendUserFailiureNotificationAsync(ERROR_OCCURED_MSG, exception.Message, userNameIdentifier);
                    }

                    else
                    {
                        context.Request.Method = HttpMethod.Get.Method;
                        context.Request.Path = "/";
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await this.next(context);
                        Thread.Sleep(110);
                        await notifyService.SendUserFailiureNotificationAsync(ERROR_OCCURED_MSG, exception.Message, userNameIdentifier);
                    }
                }
            }

            else
            {
                await this.next(context);
            }
        }
    }
}