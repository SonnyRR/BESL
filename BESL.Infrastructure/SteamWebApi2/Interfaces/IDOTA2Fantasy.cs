using Steam.Models.DOTA2;
using BESL.Infrastructure.SteamWebAPI2.Utilities;
using System.Threading.Tasks;

namespace BESL.Infrastructure.SteamWebAPI2.Interfaces
{
    public interface IDOTA2Fantasy
    {
        Task<ISteamWebResponse<PlayerOfficialInfoModel>> GetPlayerOfficialInfo(ulong steamId);

        Task<ISteamWebResponse<ProPlayerDetailModel>> GetProPlayerList();
    }
}