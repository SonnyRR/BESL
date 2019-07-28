namespace BESL.Application.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.Extensions.Logging;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;

    public class RequestExceptionNotificationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger logger;
        private readonly IUserAcessor userAcessor;

        public RequestExceptionNotificationBehaviour(ILogger<TRequest> logger, IUserAcessor userAcessor)
        {
            this.logger = logger;
            this.userAcessor = userAcessor;
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
                throw exception;
            }

            return response;
        }
    }
}
