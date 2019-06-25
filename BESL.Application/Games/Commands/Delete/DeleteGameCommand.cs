namespace BESL.Application.Games.Commands.Delete
{
    using MediatR;

    public class DeleteGameCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
