namespace BESL.Application.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR.Pipeline;
    using Microsoft.Extensions.Logging;

    using BESL.Application.Interfaces;

    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger logger;
        private readonly IUserAccessor userAccessor;

        public RequestLogger(ILogger<TRequest> logger, IUserAccessor userAccessor)
        {
            this.logger = logger;
            this.userAccessor = userAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            var userId = this.userAccessor.UserId;
            this.logger.LogInformation("BESL Request: {Name}-{userId} {@Request}", name, userId, request);

            return Task.CompletedTask;
        }
    }
}
