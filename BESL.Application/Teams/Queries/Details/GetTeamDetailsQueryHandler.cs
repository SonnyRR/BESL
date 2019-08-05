namespace BESL.Application.Teams.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetTeamDetailsQueryHandler : IRequestHandler<GetTeamDetailsQuery, GetTeamDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IMapper mapper;

        public GetTeamDetailsQueryHandler(IDeletableEntityRepository<Team> teamsRepository, IMapper mapper)
        {
            this.teamsRepository = teamsRepository;
            this.mapper = mapper;
        }

        public async Task<GetTeamDetailsViewModel> Handle(GetTeamDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamsRepository
                .AllAsNoTracking()
                .Include(t => t.TournamentFormat)
                    .ThenInclude(tf => tf.Game)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Team), request.Id);

            return this.mapper.Map<GetTeamDetailsViewModel>(desiredTeam);
        }
    }
}
