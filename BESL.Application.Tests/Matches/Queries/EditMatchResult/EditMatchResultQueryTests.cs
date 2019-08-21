namespace BESL.Application.Tests.Matches.Queries.EditMatchResult
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Commands.EditMatchResult;
    using BESL.Application.Matches.Queries.EditMatchResult;
    using Match = Domain.Entities.Match;

    public class EditMatchResultQueryTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "EditMatchResult query tests")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new EditMatchResultQuery { Id = 2 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");

            var sut = new EditMatchResultQueryHandler(this.deletableEntityRepository, userAccessorMock.Object, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeOfType<EditMatchResultCommand>();
        }

        [Trait(nameof(Match), "EditMatchResult query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditMatchResultQueryHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IUserAccessor>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "EditMatchResult query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new EditMatchResultQuery { Id = 312 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo2");

            var sut = new EditMatchResultQueryHandler(this.deletableEntityRepository, userAccessorMock.Object, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "EditMatchResult query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var query = new EditMatchResultQuery { Id = 1 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo5");

            var sut = new EditMatchResultQueryHandler(this.deletableEntityRepository, userAccessorMock.Object, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
