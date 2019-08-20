namespace BESL.Application.Tests.Teams.Commands.InvitePlayer
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
    using BESL.Application.Teams.Commands.InvitePlayer;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Persistence.Repositories;
    using static BESL.Common.GlobalConstants;

    public class InvitePlayerCommandTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given valid request should create player invite")]
        public async Task Handle_GivenValidRequest_ShouldCreateInvite()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = "FooP5" };

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var teamInvitesRepository = new EfDeletableEntityRepository<TeamInvite>(this.dbContext);
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new InvitePlayerCommandHandler(this.deletableEntityRepository, playersRepository, teamInvitesRepository, this.mediatorMock.Object, userAccessorMock.Object);

            // Act
            var rowsAffected = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            rowsAffected.ShouldBeGreaterThan(0);

            var createdInvite = teamInvitesRepository.AllAsNoTracking().FirstOrDefault(x => x.PlayerId == "Foo5");
            createdInvite.ShouldNotBeNull();
            createdInvite.TeamName.ShouldBe("FooTeam1");
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new InvitePlayerCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                It.IsAny<IDeletableEntityRepository<Player>>(),
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw InvalidSearchQueryException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowInvalidSearchQueryException()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = string.Empty };
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new InvitePlayerCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                playersRepository,
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<InvalidSearchQueryException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerDoesNotExistException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerDoesNotExistException()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = "NonExistentPlayer" };
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new InvitePlayerCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                playersRepository,
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<PlayerDoesNotExistException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerIsVacBannedException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerIsVacBannedException()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = "FooP5" };
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var desiredPlayer = this.dbContext.Players.SingleOrDefault(x => x.UserName == "FooP5");
            desiredPlayer.Claims.Add(new Microsoft.AspNetCore.Identity.IdentityUserClaim<string>() { ClaimType = IS_VAC_BANNED_CLAIM_TYPE, ClaimValue = "Yes" });
            this.dbContext.SaveChanges();

            var sut = new InvitePlayerCommandHandler(
                It.IsAny<IDeletableEntityRepository<Team>>(),
                playersRepository,
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<PlayerIsVacBannedException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1183, UserName = "FooP5" };
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new InvitePlayerCommandHandler(
                this.deletableEntityRepository,
                playersRepository,
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = "FooP5" };

            this.dbContext.PlayerTeams.Add(new PlayerTeam { PlayerId = "Foo5", TeamId = 2 });
            this.dbContext.SaveChanges();

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new InvitePlayerCommandHandler(
                this.deletableEntityRepository,
                playersRepository,
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw TeamIsFullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowTeamIsFullException()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = "FooP5" };

            for (int i = 0; i < 10; i++)
            {
                var player = new Player { UserName = $"Placeholder{i}", Email = "foomail.bg", };
                this.dbContext.Add(player);
                this.dbContext.PlayerTeams.Add(new PlayerTeam { TeamId = 1, PlayerId = player.Id });
                this.dbContext.SaveChanges();
            }

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new InvitePlayerCommandHandler(
                this.deletableEntityRepository,
                playersRepository,
                It.IsAny<IDeletableEntityRepository<TeamInvite>>(),
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<TeamIsFullException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "InvitePlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerAlreadyHasPendingInvite")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerAlreadyHasPendingInvite()
        {
            // Arrange
            var command = new InvitePlayerCommand { TeamId = 1, UserName = "FooP5" };

            this.dbContext.TeamInvites.Add(new TeamInvite { PlayerId = "Foo5", TeamId = 1 });
            this.dbContext.SaveChanges();

            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);
            var teamInvitesRepository = new EfDeletableEntityRepository<TeamInvite>(this.dbContext);

            var sut = new InvitePlayerCommandHandler(
                this.deletableEntityRepository,
                playersRepository,
                teamInvitesRepository,
                It.IsAny<IMediator>(),
                It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<PlayerAlreadyHasPendingInvite>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
