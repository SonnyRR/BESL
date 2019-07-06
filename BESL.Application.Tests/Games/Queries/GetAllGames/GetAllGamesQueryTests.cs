namespace BESL.Application.Tests.Games.Queries.GetAllGames
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Persistence;
    using Shouldly;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllGamesQueryTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllGamesQueryTests(QueryTestFixture fixture)
        {
            this.dbContext = fixture.Context;
            this.mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ShouldReturnValidViewModel()
        {
            // Arrange

            var query = new GetAllGamesQuery();
            var sut = new GetAllGamesQueryHandler(this.dbContext, this.mapper);

            var result = await sut.Handle(query, CancellationToken.None);

            result.Games.Count.ShouldBe(3);
            result.Games.Any(g => g.Name == "SampleGame2").ShouldBe(true);
        }
    }
}
