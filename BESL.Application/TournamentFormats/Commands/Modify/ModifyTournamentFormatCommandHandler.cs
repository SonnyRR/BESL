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

    public class ModifyTournamentFormatCommandHandler : IRequestHandler<ModifyTournamentFormatCommand,int>
    {
        private readonly IApplicationDbContext dbContext;

        public ModifyTournamentFormatCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Handle(ModifyTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var desiredFormat = await this.dbContext
                .TournamentFormats
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

            return await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
