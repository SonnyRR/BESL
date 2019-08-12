namespace BESL.Application.TeamTableResults.Commands.Activate
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class ActivateTeamTableResultCommandHandler : IRequestHandler<ActivateTeamTableResultCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository;

        public ActivateTeamTableResultCommandHandler(IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository)
        {
            this.teamTableResultsRepository = teamTableResultsRepository;
        }

        public async Task<int> Handle(ActivateTeamTableResultCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTableResult = await this.teamTableResultsRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(ttr => ttr.Id == request.TeamTableResultId && ttr.IsDropped == true, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamTableResult), request.TeamTableResultId);

            desiredTableResult.IsDropped = false;

            this.teamTableResultsRepository.Update(desiredTableResult);
            return await this.teamTableResultsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
