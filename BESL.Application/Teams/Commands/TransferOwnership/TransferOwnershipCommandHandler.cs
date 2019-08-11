namespace BESL.Application.Teams.Commands.TransferOwnership
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class TransferTeamOwnershipCommandHandler : IRequestHandler<TransferTeamOwnershipCommand, int>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IUserAcessor userAcessor;

        public TransferTeamOwnershipCommandHandler(IDeletableEntityRepository<Team> teamsRepository, IUserAcessor userAcessor)
        {
            this.teamsRepository = teamsRepository;
            this.userAcessor = userAcessor;
        }

        public async Task<int> Handle(TransferTeamOwnershipCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamsRepository
                .All()
                .Include(t => t.PlayerTeams)
                    .ThenInclude(pt => pt.Player)
                .SingleOrDefaultAsync(t => t.Id == request.TeamId, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.TeamId);

            if (desiredTeam.OwnerId != this.userAcessor.UserId)
            {
                throw new ForbiddenException();
            }

            if (!desiredTeam.PlayerTeams.Any(p => p.PlayerId == request.PlayerId))
            {
                throw new PlayerIsNotAMemberOfTheTeamException();
            }

            desiredTeam.OwnerId = request.PlayerId;
            this.teamsRepository.Update(desiredTeam);
            return await this.teamsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
