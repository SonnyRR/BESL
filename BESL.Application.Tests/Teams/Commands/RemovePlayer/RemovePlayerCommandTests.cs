namespace BESL.Application.Tests.Teams.Commands.RemovePlayer
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.RemovePlayer;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class RemovePlayerCommandTests : BaseTest<PlayerTeam>
    {
        [Trait(nameof(Team), "RemovePlayer command tests")]
        [Fact(DisplayName = "Handle given valid request should remove player from team")]
        public async Task Handle_GivenValidRequest_ShouldAddPlayerToTeam()
        {
            // Arrange
            var command = new RemovePlayerCommand { TeamId = 1, PlayerId = "Foo5" };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");
            var sut = new RemovePlayerCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            this.dbContext.Add(new PlayerTeam { TeamId = 1, PlayerId = "Foo5" });
            this.dbContext.SaveChanges();

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBeGreaterThan(0);

            var isPlayerRemoved = this.dbContext.PlayerTeams.Any(x => x.PlayerId == "Foo5" && x.TeamId == 1 && !x.IsDeleted);
            isPlayerRemoved.ShouldBe(false);
        }

        [Trait(nameof(Team), "RemovePlayer command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new RemovePlayerCommandHandler(It.IsAny<IDeletableEntityRepository<PlayerTeam>>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "RemovePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new RemovePlayerCommand { TeamId = 1, PlayerId = "InvalidId" };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");
            var sut = new RemovePlayerCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }


        [Trait(nameof(Team), "RemovePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var command = new RemovePlayerCommand { TeamId = 1, PlayerId = "Foo5" };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("InvalidId");
            var sut = new RemovePlayerCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            this.dbContext.Add(new PlayerTeam { TeamId = 1, PlayerId = "Foo5" });
            this.dbContext.SaveChanges();

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "RemovePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw OwnerOfAMustTransferOwnerShipBeforeLeavingTeamException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowOwnerOfAMustTransferOwnerShipBeforeLeavingTeamException()
        {
            // Arrange
            var command = new RemovePlayerCommand { TeamId = 1, PlayerId = "Foo1" };
            this.dbContext.PlayerTeams.Add(new PlayerTeam { PlayerId = "Foo5", TeamId = 1 });
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");
            var sut = new RemovePlayerCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<OwnerOfAMustTransferOwnerShipBeforeLeavingTeamException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
