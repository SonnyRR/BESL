namespace BESL.Application.Tests.Games.Queries.GetAllGamesSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Xunit;

    using BESL.Application.Games.Queries.GetAllGamesSelectList;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Persistence;
    using Shouldly;
    using BESL.Domain.Entities;

    [Collection("QueryCollection")]
    public class GetAllGamesSelectListQueryTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryTests(QueryTestFixture fixture)
        {
            this.mapper = fixture.Mapper;
            this.dbContext = fixture.Context;
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "Handler should return valid GamesSelectList viewmodel.")]
        public void Handle_ShouldReturnValidViewModel()
        {
            // Arrange
            var query = new GetAllGamesSelectListQuery();
            var sut = new GetAllGamesSelectListQueryHandler(this.dbContext, this.mapper);

            // Act
            var result = sut.Handle(query, CancellationToken.None)
                .GetAwaiter()
                .GetResult()
                .ToList();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
        }
    }
}
