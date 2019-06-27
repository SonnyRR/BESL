namespace BESL.Application.Games.Queries.ModifyGame
{
    using MediatR;

    public class ModifyGameQuery : IRequest<ModifyGameViewModel>
    {
        public int Id { get; set; }
    }
}
