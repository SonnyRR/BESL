namespace BESL.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(hostBuilder =>
                {
                    hostBuilder.ConfigureAppConfiguration((context, config) =>
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
                });
        }
    }
}