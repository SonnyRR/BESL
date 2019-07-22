namespace BESL.Application.Games.Queries.Modify
{
    using MediatR;

    public class ModifyGameQuery : IRequest<ModifyGameViewModel>
    {
        public int Id { get; set; }
    }
}
