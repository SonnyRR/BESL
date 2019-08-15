namespace BESL.Web
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using System.Globalization;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseApplicationInsights()
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                    {
                        var builtConfig = config.Build();
                        config.AddAzureKeyVault(
                            $"https://{builtConfig["KeyVault:Vault"]}.vault.azure.net/",
                            builtConfig["KeyVault:ClientId"],
                            builtConfig["KeyVault:ClientSecret"],
                            new DefaultKeyVaultSecretManager());
                    }
                })
                .UseStartup<Startup>();
    }
}