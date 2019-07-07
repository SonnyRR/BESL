namespace BESL.Application.Tests.Games.Queries.GetGameDetails
{
    using AutoMapper;
    using BESL.Application.Games.Queries.GetGameDetails;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Persistence;
    using Shouldly;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetGameDetailsQueryTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetGameDetailsQueryTests(QueryTestFixture fixture)
        {
            this.mapper = fixture.Mapper;
            this.dbContext = fixture.Context;
        }

        [Fact(DisplayName = "Handler should return valid viewmodel.")]
        public void Handle_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetGameDetailsQuery() { Id = 2 };
            var sut = new GetGameDetailsQueryHandler(this.dbContext, this.mapper);

            // Act
            var result = sut.Handle(query, CancellationToken.None).GetAwaiter().GetResult();

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldNotBeNullOrEmpty();
            result.Description.ShouldNotBeNullOrEmpty();
            result.GameImageUrl.ShouldNotBeNullOrEmpty();
            result.ShouldBeOfType(typeof(GameDetailsViewModel));
        }
    }
}
