namespace BESL.Application.Tests.Games.Queries.GetAllGames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Shouldly;
    using Xunit;

    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Persistence;
    using BESL.Domain.Entities;
    using BESL.Application.Interfaces;
    using BESL.Persistence.Repositories;

    [Collection("QueryCollection")]
    public class GetAllGamesQueryTests
    {
        private readonly IDeletableEntityRepository<Game> dbContext;
        private readonly IMapper mapper;

        public GetAllGamesQueryTests(QueryTestFixture fixture)
        {
            this.dbContext = new EfDeletableEntityRepository<Game>(fixture.Context);
            this.mapper = fixture.Mapper;
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetAllGames handler given valid request should return valid GetAllGames viewmodel.")]
        public void Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange
            var query = new GetAllGamesQuery();
            var sut = new GetAllGamesQueryHandler(this.dbContext, this.mapper);

            // Act
            var result = sut.Handle(query, CancellationToken.None).GetAwaiter().GetResult();

            // Assert
            result.Games.Count.ShouldBe(3);
            result.Games.Any(g => g.Name == "SampleGame2").ShouldBe(true);
        }
    }
}
