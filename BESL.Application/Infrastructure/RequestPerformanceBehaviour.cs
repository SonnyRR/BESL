namespace BESL.Application.Infrastructure
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch stopwatch;
        private readonly ILogger<TRequest> logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            this.stopwatch = new Stopwatch();

            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            this.stopwatch.Start();

            var response = await next();

            this.stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                // TODO: Add User Details
                logger.LogWarning("BESL Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", name, this.stopwatch.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}
