namespace BESL.Application.Teams.Queries.GetAllTeamsPaged
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using BESL.Application.Interfaces;
    using BESL.Domain.Entities;

    public class GetAllTeamsPagedQueryHandler : IRequestHandler<GetAllTeamsPagedQuery, GetAllTeamsPagedViewModel>
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IMapper mapper;

        public GetAllTeamsPagedQueryHandler(IDeletableEntityRepository<Team> teamsRepository, IMapper mapper)
        {
            this.teamsRepository = teamsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllTeamsPagedViewModel> Handle(GetAllTeamsPagedQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var currentPageEntities = await this.teamsRepository
                .AllAsNoTracking()
                .OrderByDescending(t => t.CreatedOn)
                .Skip(request.Page * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TeamLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var totalTeamsCount = this.teamsRepository
                .AllAsNoTracking()
                .Count();

            var viewModel = new GetAllTeamsPagedViewModel { Teams = currentPageEntities, TotalTeamsCount = totalTeamsCount };
            return viewModel;
        }
    }
}
