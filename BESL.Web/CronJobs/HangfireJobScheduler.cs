namespace BESL.Web.CronJobs
{
    using System;
    using Hangfire;
     
    public class HangfireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            RecurringJob.AddOrUpdate<CheckForVACBans>(x => x.CheckForBans(), CheckForVACBans.CRON_SCHEDULE);
        }
    }
}
