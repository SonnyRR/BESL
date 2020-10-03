namespace BESL.Application.Tournaments.Queries.Enroll
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
    using BESL.Entities;

    public class EnrollATeamQueryHandler : IRequestHandler<EnrollATeamQuery, EnrollATeamCommand>
    {
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public EnrollATeamQueryHandler(
            IDeletableEntityRepository<Player> playersRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Tournament> tournamentsRepository,
            IMapper mapper,
            IUserAccessor userAccessor)
        {
            this.playersRepository = playersRepository;
            this.teamsRepository = teamsRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<EnrollATeamCommand> Handle(EnrollATeamQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            var currentUserId = this.userAccessor.UserId;

            if (!await CommonCheckHelper.CheckIfPlayerExists(currentUserId, this.playersRepository))
            {
                throw new NotFoundException(nameof(Player), currentUserId);
            }

            if (await CommonCheckHelper.CheckIfPlayerIsVACBanned(currentUserId, this.playersRepository))
            {
                throw new PlayerIsVacBannedException();
            }

            var desiredTournament = await this.tournamentsRepository
                .AllAsNoTracking()
                .Include(t => t.Tables)
                    .ThenInclude(tt => tt.TeamTableResults)
                .SingleOrDefaultAsync(t => t.Id == request.TournamentId, cancellationToken)
                ?? throw new NotFoundException(nameof(Tournament), currentUserId);

            if (await CommonCheckHelper.CheckIfPlayerHasAlreadyEnrolledATeam(currentUserId, desiredTournament.FormatId, this.teamsRepository))
            {
                throw new PlayerHasAlreadyEnrolledTeamException();
            }

            var eligibleTeams = await this.teamsRepository
                .AllAsNoTracking()
                .Where(t => t.OwnerId == currentUserId && t.TournamentFormatId == desiredTournament.FormatId)
                .ProjectTo<TeamsSelectItemLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (eligibleTeams.Count == 0)
            {
                throw new PlayerHasNoEligibleTeamsToEnrollException();
            }

            var skillTables = this.mapper.Map<IList<TournamentTableSelectItemLookupModel>>(
                desiredTournament.Tables.Where(t => t.TeamTableResults.Count < t.MaxNumberOfTeams));

            if (skillTables.Count == 0)
            {
                throw new TournamentTablesAreFullException();
            }

            return new EnrollATeamCommand
            { 
                TournamentName = desiredTournament.Name,
                TournamentId = desiredTournament.Id,
                Teams = eligibleTeams,
                Tables = skillTables
            };
        }
    }
}
