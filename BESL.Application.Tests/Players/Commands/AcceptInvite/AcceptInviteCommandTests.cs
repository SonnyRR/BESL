namespace BESL.Application.Tests.Players.Commands.AcceptInvite
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Players.Commands.AcceptInvite;
    using BESL.Domain.Entities;

    public class AcceptInviteCommandTests : BaseTest<TeamInvite>
    {
        [Trait(nameof(Player), "AcceptInvite command tests")]
        [Fact(DisplayName = "Handle given valid request should accept invite")]
        public async Task Handle_GivenValidRequest_ShouldAcceptInvite()
        {
            // Arrange
            var p5 = this.dbContext.Players.SingleOrDefault(x => x.Id == "Foo5");
            p5.Invites.Add(new TeamInvite { TeamId = 1, PlayerId = "Foo5", TeamName = "FooTeam1", SenderUsername = "Foo1" });
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo5");

            var command = new AcceptInviteCommand { InviteId = p5.Invites.SingleOrDefault().Id };
            var sut = new AcceptInviteCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object, userAccessorMock.Object);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(1);
            mediatorMock.Verify(m => m.Publish(It.IsAny<AcceptedInviteNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Trait(nameof(Player), "AcceptInvite command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AcceptInviteCommandHandler(It.IsAny<IDeletableEntityRepository<TeamInvite>>(), It.IsAny<IMediator>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "AcceptInvite command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo5");

            var command = new AcceptInviteCommand { InviteId = "InvalidId" };
            var sut = new AcceptInviteCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "AcceptInvite command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var p5 = this.dbContext.Players.SingleOrDefault(x => x.Id == "Foo5");
            p5.Invites.Add(new TeamInvite { TeamId = 1, PlayerId = "Foo5", TeamName = "FooTeam1", SenderUsername = "Foo1" });
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var command = new AcceptInviteCommand { InviteId = p5.Invites.SingleOrDefault().Id };
            var sut = new AcceptInviteCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
