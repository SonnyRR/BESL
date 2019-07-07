namespace BESL.Application.TournamentFormats.Queries.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Games.Queries.GetAllGamesSelectList;   
    using BESL.Application.TournamentFormats.Commands.Create;

    public class CreateTournamentFormatQueryHandler : IRequestHandler<CreateTournamentFormatQuery, CreateTournamentFormatCommand>
    {
        private readonly IMediator mediator;

        public CreateTournamentFormatQueryHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<CreateTournamentFormatCommand> Handle(CreateTournamentFormatQuery request, CancellationToken cancellationToken)
        {
            var gamesMapped = await this.mediator.Send(new GetAllGamesSelectListQuery());

            return new CreateTournamentFormatCommand() { Games = gamesMapped };
        }
    }
}
