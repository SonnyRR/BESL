namespace BESL.Application.Games.Commands.Delete
{
    using MediatR;

    public class DeleteGameCommand : IRequest
    {
        public int Id { get; set; }

        public string GameName { get; set; }
    }
}
