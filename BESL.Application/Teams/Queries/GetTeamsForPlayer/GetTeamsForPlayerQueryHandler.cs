﻿namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using AutoMapper;
    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
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

        public async Task<TeamsForPlayerViewModel> Handle(GetTeamsForPlayerQuery request, CancellationToken cancellationToken)
        {
            var teams = await this.repository
                .AllAsNoTracking()
                    .Include(x => x.Team)
                .Where(x => x.PlayerId == request.UserId)
                .ToListAsync(cancellationToken);

            var mapped = this.mapper.Map<TeamForPlayerLookupModel[]>(teams);

            var vm = new TeamsForPlayerViewModel() { PlayerTeams = mapped };
            return vm;
        }
    }
}