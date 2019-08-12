namespace BESL.Application.Matches.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class ModifyMatchCommandHandler : IRequestHandler<ModifyMatchCommand, int>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;

        public ModifyMatchCommandHandler(IDeletableEntityRepository<Match> matchesRepository)
        {
            this.matchesRepository = matchesRepository;
        }

        public async Task<int> Handle(ModifyMatchCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .AllAsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == request.Id)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            desiredMatch.HomeTeamScore = request.HomeTeamScore;
            desiredMatch.AwayTeamScore = request.AwayTeamScore;
            desiredMatch.IsDraw = request.HomeTeamScore == request.AwayTeamScore;
            desiredMatch.ScheduledDate = request.ScheduledDate;

            desiredMatch.WinnerTeamId =
                request.HomeTeamScore > request.AwayTeamScore
                ? desiredMatch.HomeTeamId
                : request.HomeTeamScore == request.AwayTeamScore
                    ? (int?)null
                    : request.AwayTeamId;

            this.matchesRepository.Update(desiredMatch);
            return await this.matchesRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
