namespace BESL.Application.Tests.Players.Commands.DeclineInvite
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
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Players.Commands.DeclineInvite;
    using BESL.Entities;


    public class DeclineInviteCommandTests : BaseTest<TeamInvite>
    {
        [Trait(nameof(Player), "DeclineInvite command tests")]
        [Fact(DisplayName = "Handle given valid request should decline invite")]
        public async Task Handle_GivenValidRequest_ShouldDeclineInvite()
        {
            // Arrange
            var p5 = this.dbContext.Players.SingleOrDefault(x => x.Id == "Foo5");
            p5.Invites.Add(new TeamInvite { TeamId = 1, PlayerId = "Foo5", TeamName = "FooTeam1", SenderUsername = "Foo1" });
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo5");

            var command = new DeclineInviteCommand { InviteId = p5.Invites.SingleOrDefault().Id };
            var sut = new DeclineInviteCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(1);
            p5.Invites.Count(x => !x.IsDeleted).ShouldBe(0);
        }

        [Trait(nameof(Player), "DeclineInvite command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeclineInviteCommandHandler(It.IsAny<IDeletableEntityRepository<TeamInvite>>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "DeclineInvite command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo5");

            var command = new DeclineInviteCommand { InviteId = "InvalidId" };
            var sut = new DeclineInviteCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "DeclineInvite command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var p5 = this.dbContext.Players.SingleOrDefault(x => x.Id == "Foo5");
            p5.Invites.Add(new TeamInvite { TeamId = 1, PlayerId = "Foo5", TeamName = "FooTeam1", SenderUsername = "Foo1" });
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var command = new DeclineInviteCommand { InviteId = p5.Invites.SingleOrDefault().Id };
            var sut = new DeclineInviteCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
