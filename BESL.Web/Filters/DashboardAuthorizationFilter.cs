namespace BESL.Web.Filters
{
    using Hangfire.Annotations;
    using Hangfire.Dashboard;
    using BESL.Entities.Enums;

    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.IsInRole(Role.Administrator.ToString());
        }
    }
}
