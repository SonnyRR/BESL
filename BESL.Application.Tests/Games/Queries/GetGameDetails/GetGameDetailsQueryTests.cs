namespace BESL.Application.Tests.Games.Queries.GetGameDetails
{
    using AutoMapper;
    using BESL.Application.Exceptions;
    using BESL.Application.Games.Queries.GetGameDetails;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Persistence;
    using Moq;
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

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetGameDetails query handler given valid request should return valid GameDetails viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange
            var entityId = 2;
            var query = new GetGameDetailsQuery() { Id = entityId };
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            repositoryMock.Setup(m => m.GetByIdWithDeletedAsync(entityId))
                .ReturnsAsync(new Game()
                {
                    Id = entityId,
                    Name = "TF2",
                    Description = "Team Fortress 2",
                    GameImageUrl = "http://foo.bar"
                });
            var sut = new GetGameDetailsQueryHandler(repositoryMock.Object, this.mapper);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldNotBeNullOrEmpty();
            result.Description.ShouldNotBeNullOrEmpty();
            result.GameImageUrl.ShouldNotBeNullOrEmpty();
            result.ShouldBeOfType(typeof(GameDetailsViewModel));
        }


        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetGameDetails query handler given invalid request should throw NotFoundException.")]
        public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetGameDetailsQuery() { Id = 90125 };
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            repositoryMock.Setup(r => r.GetByIdWithDeletedAsync()).ReturnsAsync((Game)null);
            var sut = new GetGameDetailsQueryHandler(repositoryMock.Object, this.mapper);

            // Act
            var result = sut.Handle(query, CancellationToken.None);
            Should.Throw<NotFoundException>(result);
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetGameDetails query handler given invalid request should throw ArgumentNullException.")]
        public void Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var sut = new GetGameDetailsQueryHandler(repositoryMock.Object, this.mapper);

            // Act
            var result = sut.Handle(null, CancellationToken.None);
            Should.Throw<ArgumentNullException>(result);
        }
    }
}
