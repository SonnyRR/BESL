namespace BESL.Web.Middlewares
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using System.Threading;

    using Microsoft.AspNetCore.Http;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class NotificationHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IUserAcessor userAcessor;

        public NotificationHandlerMiddleware(RequestDelegate next, IUserAcessor userAcessor)
        {
            this.next = next;
            this.userAcessor = userAcessor;
        }

        public async Task InvokeAsync(HttpContext context, INotifyService notifyService)
        {
            await this.next(context);
        }
    }
}