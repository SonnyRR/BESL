namespace BESL.Infrastructure.CronJobs
{
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.PlayWeeks.Commands.Advance;
    using BESL.Application.Interfaces;

    public class AdvancePlayWeeks
    {
        public const string CRON_SCHEDULE = "0 0 * * SUN";

        private readonly IMediator mediator;
        private readonly IDateTime dateTime;

        public AdvancePlayWeeks(IMediator mediator, IDateTime dateTime)
        {
            this.mediator = mediator;
            this.dateTime = dateTime;
        }

        public Task AdvanceActivePlayWeeks()
        {
            var currentMachineTime = this.dateTime.UtcNow;
            return this.mediator.Send(new AdvanceActiveTournamentsPlayWeeksCommand());
        }
    }
}
