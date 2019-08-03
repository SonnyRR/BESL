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
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IMapper mapper;

        public GetTeamDetailsQueryHandler(IDeletableEntityRepository<Team> teamRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }

        public async Task<GetTeamDetailsViewModel> Handle(GetTeamDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var desiredTeam = await this.teamRepository
                .AllAsNoTrackingWithDeleted()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (desiredTeam == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            return this.mapper.Map<GetTeamDetailsViewModel>(desiredTeam);
        }
    }
}
