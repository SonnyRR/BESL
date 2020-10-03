namespace BESL.Application.TournamentFormats.Commands.Modify
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

    public class ModifyTournamentFormatCommandHandler : IRequestHandler<ModifyTournamentFormatCommand, int>
    {
        private readonly IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository;

        public ModifyTournamentFormatCommandHandler(IDeletableEntityRepository<TournamentFormat> tournamentFormatsRepository)
        {
            this.tournamentFormatsRepository = tournamentFormatsRepository;
        }

        public async Task<int> Handle(ModifyTournamentFormatCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredFormat = await this.tournamentFormatsRepository
                .AllWithDeleted()
                    .Include(tf => tf.Game)
                .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(TournamentFormat), request.Id);

            desiredFormat.Name = request.Name;
            desiredFormat.Description = request.Description;
            desiredFormat.TeamPlayersCount = request.TeamPlayersCount;
            desiredFormat.TotalPlayersCount = desiredFormat.TeamPlayersCount * TOURNAMENT_FORMAT_PLAYERS_MULTIPLIER;

            this.tournamentFormatsRepository.Update(desiredFormat);
            await this.tournamentFormatsRepository.SaveChangesAsync(cancellationToken);

            return desiredFormat.Id;
        }
    }
}
