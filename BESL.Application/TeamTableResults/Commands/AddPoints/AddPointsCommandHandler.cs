namespace BESL.Application.TeamTableResults.Commands.AddPoints
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository;

        public AddPointsCommandHandler(IDeletableEntityRepository<TeamTableResult> teamTableResultsRepository)
        {
            this.teamTableResultsRepository = teamTableResultsRepository;
        }

        public async Task<int> Handle(AddPointsCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeamTableResult = await this.teamTableResultsRepository
                .All()
                .SingleOrDefaultAsync(ttr=>ttr.TournamentTableId == request.TournamentId && ttr.TeamId == request.TeamId)
                ?? throw new NotFoundException(nameof(TeamTableResult), $"TeamId: {request.TeamId} - TournamentId = {request.TournamentId}");

            desiredTeamTableResult.Points += request.Points;

            this.teamTableResultsRepository.Update(desiredTeamTableResult);
            return await this.teamTableResultsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
