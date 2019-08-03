namespace BESL.Web.Middlewares
{
    using System.Threading.Tasks;
    using System.Net.Http;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Exceptions;
    using BESL.Web.Infrastructure;

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

                else
                {
                    context.Request.Method = HttpMethod.Get.Method;
                    await this.next(context);
                }
            }
        }
    }
}