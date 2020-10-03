namespace BESL.Application.Tests.Teams.Queries.Modify
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
    using BESL.Application.Teams.Commands.Modify;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Teams.Queries.Modify;
    using BESL.Entities;

    public class ModifyTeamQueryTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "ModifyTeam query tests")]
        [Fact(DisplayName = "Handle given valid request should return ModifyTeamCommand")]
        public async Task Handle_GivenValidRequest_ShouldReturnModifyTeamCommand()
        {
            // Arrange
            var query = new ModifyTeamQuery { Id = 1 };
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new ModifyTeamQueryHandler(this.deletableEntityRepository, userAccessorMock.Object, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeOfType<ModifyTeamCommand>();
            command.Name.ShouldBe("FooTeam1");
            command.FormatName.ShouldNotBeEmpty();
        }

        [Trait(nameof(Team), "ModifyTeam query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyTeamQueryHandler(It.IsAny<IDeletableEntityRepository<Team>>(), It.IsAny<IUserAccessor>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "ModifyTeam query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new ModifyTeamQuery { Id = 141 };
            var sut = new ModifyTeamQueryHandler(this.deletableEntityRepository, It.IsAny<IUserAccessor>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "ModifyTeam query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldForbiddenException()
        {
            // Arrange
            var query = new ModifyTeamQuery { Id = 1 };
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo123");

            var sut = new ModifyTeamQueryHandler(this.deletableEntityRepository, userAccessorMock.Object, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
