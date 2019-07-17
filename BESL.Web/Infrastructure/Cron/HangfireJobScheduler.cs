namespace BESL.Web.Infrastructure.Cron
{
    using Hangfire;
     
    public class HangfireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            RecurringJob.AddOrUpdate<CheckForVACBans>(x => x.CheckForBans(), CheckForVACBans.CRON_SCHEDULE);
        }
    }
}
