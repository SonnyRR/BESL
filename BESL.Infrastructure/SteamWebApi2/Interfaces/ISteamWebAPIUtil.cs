using Steam.Models;
using BESL.Infrastructure.SteamWebAPI2.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BESL.Infrastructure.SteamWebAPI2.Interfaces
{
    public interface ISteamWebAPIUtil
    {
        Task<ISteamWebResponse<SteamServerInfoModel>> GetServerInfoAsync();

        Task<ISteamWebResponse<IReadOnlyCollection<SteamInterfaceModel>>> GetSupportedAPIListAsync();
    }
}