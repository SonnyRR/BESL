namespace BESL.Application.Games.Commands.Delete
{
    using MediatR;

    public class DeleteGameCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string GameName { get; set; }
    }
}
