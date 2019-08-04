namespace BESL.Application.Tests.Games.Queries.ModifyGame
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Games.Queries.Modify;
    using BESL.Application.Interfaces;
    using BESL.Application.Games.Commands.Modify;
    using BESL.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using MockQueryable.Moq;

    public class ModifyGameQueryTests
    {
        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "ModifyGameQuery handler given valid request should return valid GameDetails viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange
            var entityId = 2;
            var request = new ModifyGameQuery() { Id = entityId };
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();

            var dataSet = new List<Game>()
            {
                new Game()
                {
                    Id = 2,
                    Name = It.IsAny<string>(),
                    Description = It.IsAny<string>(),
                    CreatedOn = It.IsAny<DateTime>(),
                    TournamentFormats = It.IsAny<ICollection<TournamentFormat>>(),
                    GameImageUrl = "http://vidindrinkingteam.bg/gomotarzi_everything_alcoholic.jpg"
                }
            }.AsQueryable();

            var dataSetMock = dataSet.BuildMock();

            repositoryMock.Setup(x => x.AllAsNoTracking()).Returns(dataSetMock.Object);

            var sut = new ModifyGameQueryHandler(repositoryMock.Object);

            // Act
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<ModifyGameCommand>();
            result.GameImageUrl.ShouldBe("http://vidindrinkingteam.bg/gomotarzi_everything_alcoholic.jpg");
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "ModifyGameQuery handler given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var entityId = 90125;
            var request = new ModifyGameQuery() { Id = entityId };
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();

            var dataSet = new List<Game>()
            {
                new Game()
                {
                    Id = 1,
                    Name = It.IsAny<string>(),
                    Description = It.IsAny<string>(),
                    CreatedOn = It.IsAny<DateTime>(),
                    TournamentFormats = It.IsAny<ICollection<TournamentFormat>>(),
                    GameImageUrl = It.IsAny<string>()
                }
            }.AsQueryable();

            var dataSetMock = dataSet.BuildMock();

            repositoryMock.Setup(e => e.AllAsNoTracking())
                .Returns(dataSetMock.Object);

            var sut = new ModifyGameQueryHandler(repositoryMock.Object);

            // Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(request, CancellationToken.None));
        }

        [Trait(nameof(Game), "Game query tests.")]
        [Fact(DisplayName = "ModifyGameQuery handler given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var repositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var sut = new ModifyGameQueryHandler(repositoryMock.Object);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, CancellationToken.None));
        }
    }
}