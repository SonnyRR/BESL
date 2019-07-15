namespace BESL.Application.Teams.Commands.Create
{
    using MediatR;

    public class CreateTeamCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string OwnerId { get; set; }

        public int TournamentFormatId { get; set; }
    }
}
