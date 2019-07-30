namespace BESL.Web.Middlewares
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;

    using Microsoft.AspNetCore.Http;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Web.Infrastructure;

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

            if (userNameIdentifier != null)
            {
                try
                {
                    await this.next(context);
                }
                catch (BaseCustomException ex)
                {
                    if (ex is NotFoundException)
                    {
                        var result = new ViewResult
                        {
                            ViewName = "~/Views/Shared/NotFound.cshtml",
                        };

                        await context.WriteResultAsync(result);
                    }

                    else if (context.Request.Method == HttpMethod.Post.Method)
                    {
                        context.Request.Method = HttpMethod.Get.Method;
                    }

                    else
                    {
                        // FIXME
                        context.Request.Method = HttpMethod.Get.Method;
                        context.Request.Path = "/";
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                }

                await this.next(context);
            }

            else
            {
                await this.next(context);
            }
        }
    }
}