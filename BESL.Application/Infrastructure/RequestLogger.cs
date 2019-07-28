namespace BESL.Application.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Interfaces;
    using MediatR.Pipeline;
    using Microsoft.Extensions.Logging;

    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger logger;
        private readonly IUserAcessor userAcessor;

        public RequestLogger(ILogger<TRequest> logger, IUserAcessor userAcessor)
        {
            this.logger = logger;
            this.userAcessor = userAcessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            // TODO: Add User Details
            var userId = this.userAcessor.UserId;
            this.logger.LogInformation("BESL Request: {Name}-{userId} {@Request}", name, userId, request);

            return Task.CompletedTask;
        }
    }
}
