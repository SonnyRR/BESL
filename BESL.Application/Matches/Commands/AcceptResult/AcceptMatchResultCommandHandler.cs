namespace BESL.Application.Matches.Commands.AcceptResult
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.TeamTableResults.Commands.AddPoints;

    public class AcceptMatchResultCommandHandler : IRequestHandler<AcceptMatchResultCommand, int>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IMediator mediator;

        public AcceptMatchResultCommandHandler(IDeletableEntityRepository<Match> matchesRepository, IMediator mediator)
        {
            this.matchesRepository = matchesRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(AcceptMatchResultCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredMatch = await this.matchesRepository
                .All()
                .Include(m => m.PlayWeek)
                    .ThenInclude(pw => pw.TournamentTable)
                .SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            if (desiredMatch.IsResultConfirmed)
            {
                throw new ForbiddenException();
            }

            await this.AddPointsForMatch(desiredMatch);

            desiredMatch.IsResultConfirmed = true;

            this.matchesRepository.Update(desiredMatch);
            return await this.matchesRepository.SaveChangesAsync(cancellationToken);
        }

        private async Task AddPointsForMatch(Match desiredMatch)
        {
            if (desiredMatch.WinnerTeamId.HasValue)
            {

                var winnerPoints = desiredMatch.HomeTeamScore > desiredMatch.AwayTeamScore
                    ? desiredMatch.HomeTeamScore
                    : desiredMatch.AwayTeamScore;

                var defeatedPoints = desiredMatch.HomeTeamScore > desiredMatch.AwayTeamScore
                    ? desiredMatch.AwayTeamScore
                    : desiredMatch.HomeTeamScore;

                var defetedTeamId = desiredMatch.HomeTeamScore > desiredMatch.AwayTeamScore
                    ? desiredMatch.AwayTeamId
                    : desiredMatch.HomeTeamId;

                await this.mediator.Send(new AddPointsCommand
                {
                    TeamId = (int)desiredMatch.WinnerTeamId,
                    Points = (int)winnerPoints,
                    TournamentId = desiredMatch.PlayWeek.TournamentTable.TournamentId
                });

                await this.mediator.Send(new AddPointsCommand
                {
                    TeamId = (int)defetedTeamId,
                    Points = (int)defeatedPoints,
                    TournamentId = desiredMatch.PlayWeek.TournamentTable.TournamentId
                });
            }

            else if (desiredMatch.IsDraw)
            {
                await this.mediator.Send(new AddPointsCommand
                {
                    TeamId = desiredMatch.HomeTeamId,
                    Points = (int)desiredMatch.HomeTeamScore,
                    TournamentId = desiredMatch.PlayWeek.TournamentTable.TournamentId
                });

                await this.mediator.Send(new AddPointsCommand
                {
                    TeamId = desiredMatch.AwayTeamId,
                    Points = (int)desiredMatch.AwayTeamScore,
                    TournamentId = desiredMatch.PlayWeek.TournamentTable.TournamentId
                });
            }
        }
    }
}
