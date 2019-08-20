namespace BESL.Application.Tests.Teams.Commands.TransferOwnership
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
    using BESL.Application.Teams.Commands.TransferOwnership;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class TransferOwnershipCommandTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "TransferTeamOwnership command tests")]
        [Fact(DisplayName = "Handle given valid request should transfer team ownership")]
        public async Task Handle_GivenValidRequest_ShouldTransferTeamOwnership()
        {
            // Arrange
            var command = new TransferTeamOwnershipCommand { PlayerId = "Foo5", TeamId = 1 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new TransferTeamOwnershipCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            var team = this.dbContext.Teams.SingleOrDefault(x => x.Id == 1);
            team.PlayerTeams.Add(new PlayerTeam { PlayerId = "Foo5", TeamId = 1 });
            this.dbContext.SaveChanges();

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBeGreaterThan(0);
            team.OwnerId.ShouldBe("Foo5");
        }

        [Trait(nameof(Team), "TransferTeamOwnership command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new TransferTeamOwnershipCommandHandler(It.IsAny<IDeletableEntityRepository<Team>>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "TransferTeamOwnership command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new TransferTeamOwnershipCommand { PlayerId = "Foo5", TeamId = 131 };
            var sut = new TransferTeamOwnershipCommandHandler(this.deletableEntityRepository, It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "TransferTeamOwnership command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var command = new TransferTeamOwnershipCommand { PlayerId = "Foo5", TeamId = 1 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("InvalidId");

            var sut = new TransferTeamOwnershipCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "TransferTeamOwnership command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerIsNotAMemberOfTeam")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerIsNotAMemberOfTeam()
        {
            // Arrange
            var command = new TransferTeamOwnershipCommand { PlayerId = "Foo4", TeamId = 1};

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new TransferTeamOwnershipCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<PlayerIsNotAMemberOfTeam>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
