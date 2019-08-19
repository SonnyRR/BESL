namespace BESL.Application.Tournaments.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tournaments.Commands.Modify;
    using BESL.Domain.Entities;

    public class ModifyTournamentQueryHandler : IRequestHandler<ModifyTournamentQuery, ModifyTournamentCommand>
    {
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public ModifyTournamentQueryHandler(IDeletableEntityRepository<Tournament> tournamentsRepository, IMapper mapper)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<ModifyTournamentCommand> Handle(ModifyTournamentQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .Include(x => x.Format)
                    .ThenInclude(x => x.Game)
                .FirstOrDefaultAsync(t => t.Id == request.Id)
                ?? throw new NotFoundException(nameof(Tournament), request.Id);

            var modifyCommand = this.mapper.Map<ModifyTournamentCommand>(desiredTournament);
            return modifyCommand;
        }
    }
}
