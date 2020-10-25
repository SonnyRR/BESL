using Microsoft.Extensions.Configuration;
using Serilog;

namespace BESL.Web.Extensions
{
    public static class SerilogExtensions
    {
        public static void Configure(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
            => loggerConfiguration.ReadFrom.Configuration(configuration, "Logging");
    }
}