namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using AutoMapper;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetTeamsForPlayerQueryHandler : IRequestHandler<GetTeamsForPlayerQuery, TeamsForPlayerViewModel>
    {
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<PlayerTeam> repository;

        public GetTeamsForPlayerQueryHandler(IMapper mapper, IDeletableEntityRepository<PlayerTeam> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public Task<TeamsForPlayerViewModel> Handle(GetTeamsForPlayerQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
