namespace BESL.Application.Teams.Queries.Modify
{
    using BESL.Application.Teams.Commands.Modify;
    using MediatR;

    public class ModifyTeamQuery : IRequest<ModifyTeamCommand>
    {
        public int Id { get; set; }

        public string CurrentLoggedInUserId { get; set; }
    }
}
