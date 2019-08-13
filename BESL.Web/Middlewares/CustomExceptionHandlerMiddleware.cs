namespace BESL.Web.Middlewares
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Exceptions;
    using BESL.Web.Infrastructure;
    using Microsoft.Extensions.Primitives;

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.WriteResultAsync(result);
                    }
                }

                else if (ex is ForbiddenException)
                {
                    var result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Forbidden.cshtml",
                    };

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.WriteResultAsync(result);
                    }
                }

                else
                {
                    context.Response.StatusCode = StatusCodes.Status204NoContent;
                }
            }
        }
    }
}