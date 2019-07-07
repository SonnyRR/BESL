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

    public class GetAllGamesSelectListQueryTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllGamesSelectListQueryTests(QueryTestFixture fixture)
        {
            this.dbContext = fixture.Context;
            this.mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ShouldReturnValidViewModel()
        {
            // Arrange
            var query = new GetAllGamesSelectListQuery();
            var sut = new GetAllGamesSelectListQueryHandler(this.dbContext, this.mapper);

            // Act
            var result = (await sut.Handle(query, CancellationToken.None)).ToList();

            // Assert
            result.Count.ShouldBe(3);
        }
    }
}
