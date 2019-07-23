namespace BESL.Application.Games.Queries.Modify
{
    using MediatR;
    using BESL.Application.Games.Commands.Modify;

    public class ModifyGameQuery : IRequest<ModifyGameCommand>
    {
        public int Id { get; set; }
    }
}
