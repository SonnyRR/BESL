namespace BESL.Web.Infrastructure
{
    using Hangfire;
    using BESL.Infrastructure.CronJobs;
     
    public class HangfireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            RecurringJob.AddOrUpdate<CheckForVACBans>(x => x.CheckForBans(), CheckForVACBans.CRON_SCHEDULE);
            RecurringJob.AddOrUpdate<AdvancePlayWeeks>(x => x.AdvanceActivePlayWeeks(), AdvancePlayWeeks.CRON_SCHEDULE);
        }
    }
}
