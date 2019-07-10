namespace BESL.Application.Tests.Games.Queries.GetGameDetails
{
    using System.Threading;

    using AutoMapper;
    using Shouldly;
    using Xunit;

    using BESL.Application.Games.Queries.GetGameDetails;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Persistence;
    using BESL.Application.Exceptions;
    using System;

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

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "Handler givend valid request should return valid GameDetails viewmodel.")]
        public void Handle_GivenValidRequest_ShouldReturnValidViewModel()
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


        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "Handler given invalid request should throw NotFoundException.")]
        public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetGameDetailsQuery() { Id = 90125 };
            var sut = new GetGameDetailsQueryHandler(this.dbContext, this.mapper);

            // Act
            var result = sut.Handle(query, CancellationToken.None);
            Should.Throw<NotFoundException>(result);
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "Handler given invalid request should throw ArgumentNullException.")]
        public void Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetGameDetailsQueryHandler(this.dbContext, this.mapper);

            // Act
            var result = sut.Handle(null, CancellationToken.None);
            Should.Throw<ArgumentNullException>(result);
        }
    }
}
