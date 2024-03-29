﻿using Steam.Models.SteamEconomy;
using BESL.Infrastructure.SteamWebAPI2.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BESL.Infrastructure.SteamWebAPI2.Interfaces
{
    public interface ISteamEconomy
    {
        Task<ISteamWebResponse<AssetClassInfoResultModel>> GetAssetClassInfoAsync(uint appId, IReadOnlyList<ulong> classIds, string language);

        Task<ISteamWebResponse<AssetPriceResultModel>> GetAssetPricesAsync(uint appId, string currency, string language);
    }
}