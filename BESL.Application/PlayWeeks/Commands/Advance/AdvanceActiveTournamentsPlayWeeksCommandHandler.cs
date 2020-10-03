namespace BESL.Application.PlayWeeks.Commands.Advance
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    
    public class AdvanceActiveTournamentsPlayWeeksCommandHandler : IRequestHandler<AdvanceActiveTournamentsPlayWeeksCommand, int>
    {
        private readonly IDeletableEntityRepository<PlayWeek> playWeeksRepository;

        public AdvanceActiveTournamentsPlayWeeksCommandHandler(IDeletableEntityRepository<PlayWeek> playWeeksRepository)
        {
            this.playWeeksRepository = playWeeksRepository;
        }

        public async Task<int> Handle(AdvanceActiveTournamentsPlayWeeksCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var activeWeeks = await this.playWeeksRepository
                .AllAsNoTracking()
                .Include(w => w.TournamentTable)
                    .ThenInclude(tt => tt.Tournament)
                .Where(x => x.IsActive && x.TournamentTable.Tournament.IsActive)
                .ToListAsync(cancellationToken);

            foreach (var week in activeWeeks)
            {
                week.IsActive = false;
                var nextWeek = new PlayWeek { StartDate = week.EndDate.Date, IsActive = true, TournamentTableId = week.TournamentTableId };
                
                this.playWeeksRepository.Update(week);
                
                await this.playWeeksRepository.AddAsync(nextWeek);
            }

            return await this.playWeeksRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
