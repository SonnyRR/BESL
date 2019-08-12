namespace BESL.Application.Matches.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, int>
    {
        private readonly IDeletableEntityRepository<Match> matchesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public CreateMatchCommandHandler(IDeletableEntityRepository<Match> matchesRepository, IDeletableEntityRepository<Team> teamsRepository)
        {
            this.matchesRepository = matchesRepository;
            this.teamsRepository = teamsRepository;
        }

        public async Task<int> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfTeamExists(request.HomeTeamId, teamsRepository))
            {
                throw new NotFoundException(nameof(Team), request.HomeTeamId);
            }

            if (!await CommonCheckHelper.CheckIfTeamExists(request.AwayTeamId, teamsRepository))
            {
                throw new NotFoundException(nameof(Team), request.AwayTeamId);
            }

            var match = new Match { HomeTeamId = request.HomeTeamId, AwayTeamId = request.AwayTeamId, ScheduledDate = request.PlayDate, PlayWeekId = request.PlayWeekId };

            await this.matchesRepository.AddAsync(match);
            return await this.matchesRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
