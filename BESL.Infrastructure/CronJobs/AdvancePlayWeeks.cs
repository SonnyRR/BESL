namespace BESL.Infrastructure.CronJobs
{
    using System;
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
            var currentMachineTime = DateTime.UtcNow;

            if (currentMachineTime.DayOfWeek == DayOfWeek.Sunday
                && currentMachineTime.TimeOfDay <= new TimeSpan(3, 0, 0))
            {
                return this.mediator.Send(new AdvanceActiveTournamentsPlayWeeksCommand());
            }

            return Task.CompletedTask;
        }
    }
}
