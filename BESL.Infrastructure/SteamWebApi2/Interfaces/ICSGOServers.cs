using Steam.Models.CSGO;
using BESL.Infrastructure.SteamWebAPI2.Utilities;
using System.Threading.Tasks;

namespace BESL.Infrastructure.SteamWebAPI2.Interfaces
{
    public interface ICSGOServers
    {
        Task<ISteamWebResponse<ServerStatusModel>> GetGameServerStatusAsync();
    }
}