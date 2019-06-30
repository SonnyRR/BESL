namespace BESL.Application.Formats.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BESL.Application.Interfaces;
    using MediatR;

    public class CreateTournamentFormatHandler : IRequestHandler<CreateTournamentFormatCommand, int>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateTournamentFormatHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> Handle(CreateTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
