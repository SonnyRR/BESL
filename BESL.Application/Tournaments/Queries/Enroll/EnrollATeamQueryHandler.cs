﻿namespace BESL.Application.Tournaments.Queries.Enroll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Exceptions;
    using BESL.Application.Infrastructure;
    using BESL.Application.Interfaces;
    using BESL.Application.Tournaments.Commands.Enroll;
    using BESL.Domain.Entities;

    public class EnrollATeamQueryHandler : IRequestHandler<EnrollATeamQuery, EnrollATeamCommand>
    {
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;

        public EnrollATeamQueryHandler(
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Tournament> tournamentsRepository,
            IMapper mapper)
        {
            this.playersRepository = playersRepository;
            this.teamsRepository = teamsRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
        }

        public async Task<EnrollATeamCommand> Handle(EnrollATeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckHelper.CheckIfUserExists(request.UserId, playersRepository))
            {
                throw new NotFoundException(nameof(Player), request.UserId);
            }

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .Include(t => t.Tables)
                    .ThenInclude(tt => tt.TeamTableResults)
                .SingleOrDefaultAsync(t => t.Id == request.TournamentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), request.UserId);

            var eligibleTeams = await this.teamsRepository
                .AllAsNoTracking()
                .Where(t => t.OwnerId == request.UserId && t.TournamentFormatId == desiredTournament.FormatId)
                .ProjectTo<TeamsSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (eligibleTeams.Count == 0)
            {
                throw new PlayerHasNoEligibleTeamsToEnrollException();   
            }

            var skillTables = this.mapper.Map<IList<TournamentTableSelectItemLookupModel>>(desiredTournament.Tables.Where(t => t.TeamTableResults.Count < t.MaxNumberOfTeams));

            if (skillTables.Count == 0)
            {
                throw new TournamentTablesAreFullException();
            }

            return new EnrollATeamCommand
            {
                UserId = request.UserId,
                TournamentName = desiredTournament.Name,
                TournamentId = desiredTournament.Id,
                Teams = eligibleTeams,
                Tables = skillTables
            };
        }
    }
}