namespace BESL.Application.Tests.Games.Queries.GetAllGamesSelectList
{
    using System.Linq;
    using System.Threading;

    using AutoMapper;
    using Xunit;
    using Shouldly;
    using System.Threading.Tasks;

    using BESL.Application.Games.Queries.GetAllGamesSelectList;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Persistence;
    using BESL.Domain.Entities;
    using BESL.Application.Interfaces;
    using BESL.Persistence.Repositories;


    [Collection("QueryCollection")]
    public class GetAllGamesSelectListQueryTests
    {
        private readonly IMapper mapper;
        private readonly IApplicationDbContext dbContext;

        public GetAllGamesSelectListQueryTests(QueryTestFixture fixture)
        {
            this.mapper = fixture.Mapper;
            this.dbContext = fixture.Context;
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetAllGames handler given valid request should return valid GamesSelectList viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange           
            var query = new GetAllGamesSelectListQuery();
            var sut = new GetAllGamesSelectListQueryHandler(new EfDeletableEntityRepository<Game>((ApplicationDbContext)this.dbContext), this.mapper);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count().ShouldBe(3);
        }
    }
}
