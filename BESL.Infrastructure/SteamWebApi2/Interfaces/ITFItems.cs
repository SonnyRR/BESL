using Steam.Models.TF2;
using BESL.Infrastructure.SteamWebAPI2.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BESL.Infrastructure.SteamWebAPI2.Interfaces
{
    public interface ITFItems
    {
        Task<ISteamWebResponse<IReadOnlyCollection<GoldenWrenchModel>>> GetGoldenWrenchesAsync();
    }
}