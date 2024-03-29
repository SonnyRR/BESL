﻿namespace BESL.Application.TournamentFormats.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    public class DeleteTournamentFormatCommandHandler : IRequestHandler<DeleteTournamentFormatCommand, int>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;

        public DeleteTournamentFormatCommandHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
        }

        public async Task<int> Handle(DeleteTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredFormat = await this.tournamentFormatsRepository
                .AllWithDeleted()
                .SingleOrDefaultAsync(t => t.Id == request.Id)
                ?? throw new NotFoundException(nameof(TournamentFormat), request.Id);

            if (desiredFormat.IsDeleted)
            {
                throw new DeleteFailureException(nameof(TournamentFormat), desiredFormat.Id, ENTITY_ALREADY_DELETED_MSG);
            }

            this.tournamentFormatsRepository.Delete(desiredFormat);
            await this.tournamentFormatsRepository.SaveChangesAsync(cancellationToken);

            return desiredFormat.Id;
        }
    }
}
