using Steam.Models;
using BESL.Infrastructure.SteamWebAPI2.Utilities;
using System.Threading.Tasks;

namespace BESL.Infrastructure.SteamWebAPI2.Interfaces
{
    public interface IGCVersion
    {
        Task<ISteamWebResponse<GameClientResultModel>> GetClientVersionAsync();

        Task<ISteamWebResponse<GameClientResultModel>> GetServerVersionAsync();
    }
}