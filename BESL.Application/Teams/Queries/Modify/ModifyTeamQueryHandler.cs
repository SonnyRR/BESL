namespace BESL.Application.Teams.Queries.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.Modify;
    using BESL.Domain.Entities;
    using BESL.Application.Exceptions;

    public class ModifyTeamQueryHandler : IRequestHandler<ModifyTeamQuery, ModifyTeamCommand>
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IMapper mapper;

        public ModifyTeamQueryHandler(IDeletableEntityRepository<Team> teamRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }

        public async Task<ModifyTeamCommand> Handle(ModifyTeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                    .ThenInclude(tf => tf.Game)
                .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.Id);

            var command = this.mapper.Map<ModifyTeamCommand>(desiredTeam);
            return command;
        }
    }
}
