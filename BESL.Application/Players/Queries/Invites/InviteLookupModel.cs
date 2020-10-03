namespace BESL.Application.Players.Queries.Invites
{
    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class InviteLookupModel : IMapFrom<TeamInvite>
    {
        public string Id { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string SenderUsername { get; set; }
    }
}
