namespace BESL.Application.TeamTableResults.Commands.Drop
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class DropTeamTableResultCommandHandler : IRequestHandler<DropTeamTableResultCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository;

        public DropTeamTableResultCommandHandler(IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository)
        {
            this.teamTableResultsRepository = teamTableResultsRepository;
        }

        public async Task<int> Handle(DropTeamTableResultCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTableResult = await this.teamTableResultsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(ttr => ttr.Id == request.TeamTableResultId && ttr.IsDropped == false, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamTableResult), request.TeamTableResultId);

            desiredTableResult.IsDropped = true;

            this.teamTableResultsRepository.Update(desiredTableResult);
            return await this.teamTableResultsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
