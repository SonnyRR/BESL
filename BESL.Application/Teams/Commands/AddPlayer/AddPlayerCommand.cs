namespace BESL.Application.Teams.Commands.AddPlayer
{
    using MediatR;

    public class AddPlayerCommand : IRequest<int>
    {
        public int TeamId { get; set; }

        public string PlayerId { get; set; }
    }
}
