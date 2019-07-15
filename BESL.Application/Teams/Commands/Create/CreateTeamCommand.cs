namespace BESL.Application.Teams.Commands.Create
{
    using System;
    using MediatR;

    public class CreateTeamCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public object MyProperty { get; set; }

        public string OwnerId { get; set; }
    }
}
