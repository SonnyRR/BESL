namespace BESL.Application.Tournaments.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;

    using BESL.Application.Interfaces;
    using System.Linq;

    public class CreateTournamentCommandHandler : IRequestHandler<CreateTournamentCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateTournamentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<Unit> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            if (!this.context.Tournaments.Any(t=>t.Name == request.Name))
            {

            }
        }
    }
}
