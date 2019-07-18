namespace BESL.Common.SteamWebApi
{
    using Microsoft.Extensions.Configuration;
    using SteamWebAPI2.Interfaces;

    public class SteamApiHelper
    {
        #warning Don't forget to add your Steam API key to secrets. https://steamcommunity.com/dev/apikey
        // dotnet user-secrets set "steam-api-key" "steam_api_key"

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
