﻿namespace BESL.Application.Players.Commands.DeclineInvite
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class DeclineInviteCommandHandler : IRequestHandler<DeclineInviteCommand, int>
    {
        private readonly IDeletableEntityRepository<TeamInvite> teamInvitesRepository;

        public DeclineInviteCommandHandler(IDeletableEntityRepository<TeamInvite> teamInvitesRepository)
        {
            this.teamInvitesRepository = teamInvitesRepository;
        }

        public async Task<int> Handle(DeclineInviteCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredInvite = await this.teamInvitesRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == request.InviteId, cancellationToken)
                ?? throw new NotFoundException(nameof(TeamInvite), request.InviteId);

            if (desiredInvite.PlayerId != request.UserId)
            {
                throw new ForbiddenException();
            }

            this.teamInvitesRepository.Delete(desiredInvite);
            return await this.teamInvitesRepository.SaveChangesAsync(cancellationToken);
        }
    }
}