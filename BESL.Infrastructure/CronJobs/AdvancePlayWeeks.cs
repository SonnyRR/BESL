namespace BESL.Infrastructure.CronJobs
{
    using System.Threading.Tasks;
    using MediatR;
    using BESL.Application.PlayWeeks.Commands.Advance;

    public class AdvancePlayWeeks
    {
        public const string CRON_SCHEDULE = "0 0 * * SUN";

        private readonly IMediator mediator;

        public AdvancePlayWeeks(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task AdvanceActivePlayWeeks()
        {
            return this.mediator.Send(new AdvanceActiveTournamentsPlayWeeksCommand());
        }
    }
}
