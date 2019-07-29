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
        private readonly IUserAcessor userAcessor;

        public RequestLogger(ILogger<TRequest> logger, IUserAcessor userAcessor)
        {
            this.logger = logger;
            this.userAcessor = userAcessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            var userId = this.userAcessor.UserId;
            this.logger.LogInformation("BESL Request: {Name}-{userId} {@Request}", name, userId, request);

            return Task.CompletedTask;
        }
    }
}
