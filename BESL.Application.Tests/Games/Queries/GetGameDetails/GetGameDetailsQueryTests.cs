namespace BESL.Application.Tests.Games.Queries.GetGameDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using MockQueryable.Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Games.Queries.Details;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    [Collection("QueryCollection")]
    public class GetGameDetailsQueryTests
    {
        private readonly IDeletableEntityRepository<Game> gameRepository;
        private readonly IMapper mapper;

        public GetGameDetailsQueryTests(QueryTestFixture fixture)
        {
            this.mapper = fixture.Mapper;
            this.gameRepository = new EfDeletableEntityRepository<Game>(fixture.Context);
        }

        [Trait(nameof(Game), "GetGameDetails query tests.")]
        [Fact(DisplayName = "Handle given valid request should return valid GameDetails viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange
            var entityId = 2;
            var query = new GetGameDetailsQuery() { Id = entityId };
            var gameRepositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var resultData = new List<Game>()
            {
                new Game()
                {
                    Id = entityId,
                    Name = "TestGame",
                    Description = "TesxtGameDescription",
                    GameImageUrl = "https://www.foo.bar/thumbnail.jpg",
                    CreatedOn = new DateTime(2019, 05, 21)
                }
            };

            var resultDataMock = resultData.AsQueryable().BuildMock();
            gameRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(resultDataMock.Object);

            var sut = new GetGameDetailsQueryHandler(gameRepositoryMock.Object, this.mapper);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldNotBeNullOrEmpty();
            result.Description.ShouldNotBeNullOrEmpty();
            result.GameImageUrl.ShouldNotBeNullOrEmpty();
            result.ShouldBeOfType(typeof(GameDetailsViewModel));
        }


        [Trait(nameof(Game), "GetGameDetails query tests.")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetGameDetailsQuery() { Id = 90125 };
            var gameRepositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var resultData = new List<Game>()
            {
                new Game()
                {
                    Id = 3
                }
            };

            var resultDataMock = resultData.AsQueryable().BuildMock();
            gameRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(resultDataMock.Object);

            var sut = new GetGameDetailsQueryHandler(gameRepositoryMock.Object, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, CancellationToken.None));
        }

        [Trait(nameof(Game), "GetGameDetails query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var sut = new GetGameDetailsQueryHandler(repositoryMock.Object, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, CancellationToken.None));
        }
    }
}
