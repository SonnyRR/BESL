namespace BESL.Application.Infrastructure
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Interfaces;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch stopwatch;
        private readonly ILogger<TRequest> logger;
        private readonly IUserAcessor userAcessor;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, IUserAcessor userAcessor)
        {
            this.stopwatch = new Stopwatch();
            this.logger = logger;
            this.userAcessor = userAcessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            this.stopwatch.Start();

            var response = await next();

            this.stopwatch.Stop();

            if (this.stopwatch.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                this.logger.LogWarning("BESL Long Running Request: {Name}-[UserId: {userId}] ({ElapsedMilliseconds} milliseconds) {@Request}", name, this.userAcessor.UserId, this.stopwatch.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}
