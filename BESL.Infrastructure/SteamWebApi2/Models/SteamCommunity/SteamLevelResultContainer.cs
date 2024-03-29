﻿using Newtonsoft.Json;

namespace BESL.Infrastructure.SteamWebAPI2.Models.SteamCommunity
{
    internal class SteamLevelResult
    {
        [JsonProperty("player_level")]
        public uint PlayerLevel { get; set; }
    }

    internal class SteamLevelResultContainer
    {
        [JsonProperty("response")]
        public SteamLevelResult Result { get; set; }
    }
}