namespace BESL.Web.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedMiddleware>();
        }
    }
}
