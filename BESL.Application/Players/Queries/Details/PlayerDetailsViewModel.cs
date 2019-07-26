namespace BESL.Application.Players.Queries.Details
{
    using System.Linq;

    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;

    public class PlayerDetailsViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public bool IsVACBanned { get; set; }

        public string AvatarFullUrl { get; set; }

        public string SteamId64 { get; set; }

        public string RegisteredOn { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Player, PlayerDetailsViewModel>()
                .ForMember(pdvm => pdvm.IsVACBanned, o => o.MapFrom(p => p.Claims.Any(c => c.ClaimType == IS_VAC_BANNED_CLAIM_TYPE)))
                .ForMember(pdvm => pdvm.AvatarFullUrl, o => o.MapFrom(p => p.Claims.SingleOrDefault(c => c.ClaimType == PROFILE_AVATAR_CLAIM_TYPE).ClaimValue))
                .ForMember(pdvm => pdvm.SteamId64, o => o.MapFrom(p => p.Claims.SingleOrDefault(c => c.ClaimType == STEAM_ID_64_CLAIM_TYPE).ClaimValue))
                .ForMember(pdvm => pdvm.Username, o => o.MapFrom(p => p.UserName))
                .ForMember(pdvm => pdvm.RegisteredOn, o => o.MapFrom(p => p.CreatedOn.ToShortDateString()));
        }
    }
}
