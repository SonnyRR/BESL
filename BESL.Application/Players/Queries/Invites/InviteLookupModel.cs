namespace BESL.Application.Players.Queries.Invites
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class InviteLookupModel : IMapFrom<TeamInvite>
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string SenderUsername { get; set; }
    }
}
