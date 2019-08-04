namespace BESL.Application.Teams.Queries.GetPlayersForTeam
{
    using System.Linq;

    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class PlayerLookup : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string AvatarSmall { get; set; }

        public string SteamId64 { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Player, PlayerLookup>()
                .ForMember(lm => lm.AvatarSmall, o => o.MapFrom(src => src.Claims.SingleOrDefault(y => y.ClaimType == PROFILE_AVATAR_MEDIUM_CLAIM_TYPE).ClaimValue))
                .ForMember(lm => lm.AvatarSmall, o => o.MapFrom(src => src.Claims.SingleOrDefault(y => y.ClaimType == STEAM_ID_64_CLAIM_TYPE).ClaimValue));
        }
    }
}
