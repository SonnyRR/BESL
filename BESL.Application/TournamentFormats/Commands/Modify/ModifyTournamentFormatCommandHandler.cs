namespace BESL.Application.TournamentFormats.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class ModifyTournamentFormatCommandHandler : IRequestHandler<ModifyTournamentFormatCommand, int>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> repository;

        public ModifyTournamentFormatCommandHandler(IDeletableEntityRepository<TournamentFormat> repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(ModifyTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredFormat = await this.repository
                .AllWithDeleted()
                    .Include(tf => tf.Game)
                .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (desiredFormat == null)
            {
                throw new NotFoundException(nameof(TournamentFormat), request.Id);
            }

            desiredFormat.Name = request.Name;
            desiredFormat.Description = request.Description;
            desiredFormat.TeamPlayersCount = request.TeamPlayersCount;
            desiredFormat.ModifiedOn = DateTime.UtcNow;

            this.repository.Update(desiredFormat);
            return await this.repository.SaveChangesAsync(cancellationToken);
        }
    }
}
