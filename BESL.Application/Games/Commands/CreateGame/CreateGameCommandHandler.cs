namespace BESL.Application.Games.Commands.CreateGame
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateGameCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            Game game = new Game()
            {
                Name = request.Name,
                Description = request.Description,
            };

            this.context.Games.Add(game);
            await this.context.SaveChangesAsync(cancellationToken);

            return game.Id;
        }
    }
}
