namespace BESL.Common.SteamWebApi
{
    using Microsoft.Extensions.Configuration;
    using SteamWebAPI2.Interfaces;

    public class SteamApiHelper
    {
        public static ISteamUser GetSteamUserInstance(IConfiguration configuration)
        {
            return new SteamUser(configuration["steam-api-key"]);
        }

        public static ISteamApps GetSteamAppsInstance(IConfiguration configuration)
        {
            return new SteamApps(configuration["steam-api-key"]);
        }
    }
}
