namespace BESL.Application.TournamentFormats.Commands.Modify
{
    using MediatR;

    public class ModifyTournamentFormatCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TeamPlayersCount { get; set; }
    }
}
