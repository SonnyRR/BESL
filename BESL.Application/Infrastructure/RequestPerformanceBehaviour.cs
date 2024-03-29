﻿namespace BESL.Application.Infrastructure
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.Extensions.Logging;

    using BESL.Application.Interfaces;

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch stopwatch;
        private readonly ILogger<TRequest> logger;
        private readonly IUserAccessor userAccessor;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, IUserAccessor userAccessor)
        {
            this.stopwatch = new Stopwatch();
            this.logger = logger;
            this.userAccessor = userAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            this.stopwatch.Start();

            var response = await next();

            this.stopwatch.Stop();

            if (this.stopwatch.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                this.logger.LogWarning("BESL Long Running Request: {Name}-[UserId: {userId}] ({ElapsedMilliseconds} milliseconds) {@Request}", name, this.userAccessor.UserId, this.stopwatch.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}
