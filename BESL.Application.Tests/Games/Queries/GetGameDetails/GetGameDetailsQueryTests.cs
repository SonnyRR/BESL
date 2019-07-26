namespace BESL.Application.Tests.Games.Queries.GetGameDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;
    using MockQueryable.Moq;

    using BESL.Application.Exceptions;
    using BESL.Application.Games.Queries.Details;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
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

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetGameDetails query handler given valid request should return valid GameDetails viewmodel.")]
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


        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "GetGameDetails query handler given invalid request should throw NotFoundException.")]
        public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
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
