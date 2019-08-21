namespace BESL.Application.Tests.Players.Queries.Invites
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.Players.Queries.Invites;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class GetInvitesForPlayerQueryTests : BaseTest<TeamInvite>
    {
        [Trait(nameof(Player), "GetInvitesForPlayer query tests")]
        [Fact(DisplayName = "Handle given valid request should accept invite")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var p5 = this.dbContext.Players.SingleOrDefault(x => x.Id == "Foo5");
            p5.Invites.Add(new TeamInvite { TeamId = 1, PlayerId = "Foo5", TeamName = "FooTeam1", SenderUsername = "Foo1" });
            this.dbContext.SaveChanges();

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo5");

            var command = new GetInvitesForPlayerQuery();
            var sut = new GetInvitesForPlayerQueryHandler(this.deletableEntityRepository, this.mapper, userAccessorMock.Object);

            // Act
            var viewModel = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Invites.Count().ShouldBe(1);
        }

        [Trait(nameof(Player), "GetInvitesForPlayer query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldReturnViewModel()
        {
            // Arrange
            var sut = new GetInvitesForPlayerQueryHandler(It.IsAny<IDeletableEntityRepository<TeamInvite>>(), It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
