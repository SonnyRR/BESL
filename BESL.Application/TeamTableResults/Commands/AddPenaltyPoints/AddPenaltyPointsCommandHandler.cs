namespace BESL.Application.TeamTableResults.Commands.AddPenaltyPoints
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Entities;

    public class AddPenaltyPointsCommandHandler : IRequestHandler<AddPenaltyPointsCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository;

        public AddPenaltyPointsCommandHandler(IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository)
        {
            this.teamTableResultsRepository = teamTableResultsRepository;
        }

        public async Task<int> Handle(AddPenaltyPointsCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeamTableResult = await this.teamTableResultsRepository
                .All()
                .SingleOrDefaultAsync(ttr => ttr.Id == request.TeamTableResultId, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamTableResult), request.TeamTableResultId);

            desiredTeamTableResult.PenaltyPoints += request.PenaltyPoints;

            this.teamTableResultsRepository.Update(desiredTeamTableResult);
            return await this.teamTableResultsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
