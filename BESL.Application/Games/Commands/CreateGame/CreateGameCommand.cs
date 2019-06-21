namespace BESL.Application.Games.Commands.CreateGame
{
    using MediatR;

    public class CreateGameCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
