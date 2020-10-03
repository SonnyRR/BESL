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
    using BESL.Application.Exceptions;
    using BESL.Entities;

    public class ModifyTeamQueryHandler : IRequestHandler<ModifyTeamQuery, ModifyTeamCommand>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IUserAccessor userAccessor;
        private readonly IMapper mapper;

        public ModifyTeamQueryHandler(IDeletableEntityRepository<Team> teamsRepository, IUserAccessor userAccessor, IMapper mapper)
        {
            this.teamsRepository = teamsRepository;
            this.userAccessor = userAccessor;
            this.mapper = mapper;
        }

        public async Task<ModifyTeamCommand> Handle(ModifyTeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                    .ThenInclude(tf => tf.Game)
                .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.Id);

            if (desiredTeam.OwnerId != this.userAccessor.UserId)
            {
                throw new ForbiddenException();
            }

            var command = this.mapper.Map<ModifyTeamCommand>(desiredTeam);
            return command;
        }
    }
}
