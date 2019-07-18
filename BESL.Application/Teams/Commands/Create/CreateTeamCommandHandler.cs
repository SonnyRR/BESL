namespace BESL.Application.Teams.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand>
    {
        private readonly IDeletableEntityRepository<Team> teams;

        public CreateTeamCommandHandler(IDeletableEntityRepository<Team> teams)
        {
            this.teams = teams;
        }

        public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
