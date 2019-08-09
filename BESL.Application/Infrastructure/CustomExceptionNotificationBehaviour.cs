namespace BESL.Application.Infrastructure
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using MediatR;
    using Microsoft.Extensions.Logging;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Domain.Entities.Enums;
    using static BESL.Common.GlobalConstants;

    public class CustomExceptionNotificationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger logger;
        private readonly IUserAcessor userAcessor;
        private readonly IRedisService<Notification> redisService;

        public CustomExceptionNotificationBehaviour(
            ILogger<TRequest> logger,
            IUserAcessor userAcessor,
            IRedisService<Notification> redisService)
        {
            this.logger = logger;
            this.userAcessor = userAcessor;
            this.redisService = redisService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;

            try
            {
                response = await next();
            }
            catch (BaseCustomException exception)
            {
                this.logger.LogError("BESL Request: {Name}-[UserId: {userId}] {@Request}, {@exception}", typeof(TRequest).Name, this.userAcessor.UserId, request, exception);

                if (this.userAcessor.UserId != null && !(exception is NotFoundException) && !(exception is ForbiddenException))
                {
                    var notification = new Notification()
                    {
                        Type = NotificationType.Error,
                        CreatedOn = DateTime.UtcNow,
                        Header = ERROR_OCCURED_MSG,
                        PlayerId = this.userAcessor.UserId,
                        Content = HttpUtility.HtmlEncode(exception.Message)
                    };

                    await this.redisService.Save(this.userAcessor.UserId, notification, TimeSpan.FromSeconds(REDIS_NOTIFICATION_EXPIRATION_MINUTES));
                }

                throw exception;
            }

            return response;
        }
    }
}