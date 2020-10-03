namespace BESL.Application.Matches.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Entities;

    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand, int>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;

        public DeleteMatchCommandHandler(IDeletableEntityRepository<Match> matchesRepository)
        {
            this.matchesRepository = matchesRepository;
        }

        public async Task<int> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .All()
                .SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            this.matchesRepository.Delete(desiredMatch);
            await this.matchesRepository.SaveChangesAsync(cancellationToken);

            return desiredMatch.Id;
        }
    }
}
