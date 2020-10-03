namespace BESL.Application.Tests.Matches.Commands.EditMatchResult
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Commands.EditMatchResult;
    using BESL.Entities;
    using Match = BESL.Entities.Match;

    public class EditMatchResultCommandTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "EditMatchResultCommand tests")]
        [Fact(DisplayName = "Handle given valid request should edit match")]
        public async Task Hanlde_GivenValidRequest_ShouldDeleteMatch()
        {
            // Arrange
            var participantIds = new List<string> { "Foo1", "Foo2"};
            for (int i = 1; i <= 5; i++)
            {
                var currentPlayer = new Player { UserName = $"MatchParticipant{i}", Email = "tt@tt.tt", PasswordHash = "asdf", };
                this.dbContext.Players.Add(currentPlayer);
                this.dbContext.SaveChanges();

                currentPlayer.PlayerTeams.Add(new PlayerTeam { PlayerId = currentPlayer.Id, TeamId = 1 });
                this.dbContext.SaveChanges();

                participantIds.Add(currentPlayer.Id);
            }

            for (int i = 1; i <= 5; i++)
            {
                var currentPlayer = new Player { UserName = $"MatchParticipant{i + 5}", Email = "tt@tt.tt", PasswordHash = "asdf", };
                this.dbContext.Players.Add(currentPlayer);
                this.dbContext.SaveChanges();

                currentPlayer.PlayerTeams.Add(new PlayerTeam { PlayerId = currentPlayer.Id, TeamId = 2 });
                this.dbContext.SaveChanges();

                participantIds.Add(currentPlayer.Id);
            }

            var command = new EditMatchResultCommand { Id = 1, ParticipatedPlayersIds = participantIds, AwayTeamScore = 2, HomeTeamScore = 2 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new EditMatchResultCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(11);

            var matchModified = this.dbContext.Matches.SingleOrDefault(x => x.Id == 1);
            matchModified.ParticipatedPlayers.Count.ShouldBe(12);
        }

        [Trait(nameof(Match), "EditMatchResultCommand tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Hanlde_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditMatchResultCommandHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "EditMatchResultCommand tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new EditMatchResultCommand { Id = 133 };
            var sut = new EditMatchResultCommandHandler(this.deletableEntityRepository, It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "EditMatchResultCommand tests")]
        [Fact(DisplayName = "Handle given valid request should throw InvalidMatchParticipantsCountException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowInvalidMatchParticipantsCountException()
        {
            // Arrange
            var participantIds = new List<string> { "Foo1", "Foo2" };

            var command = new EditMatchResultCommand { Id = 1, ParticipatedPlayersIds = participantIds, AwayTeamScore = 2, HomeTeamScore = 2 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new EditMatchResultCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<InvalidMatchParticipantsCountException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "EditMatchResultCommand tests")]
        [Fact(DisplayName = "Handle given valid request should throw ForbiddenException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var command = new EditMatchResultCommand { Id = 1, AwayTeamScore = 2, HomeTeamScore = 2 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("NotAnOwnerId");

            var sut = new EditMatchResultCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "EditMatchResultCommand tests")]
        [Fact(DisplayName = "Handle given valid request should throw PlayerIsNotAMemberOfTeamException")]
        public async Task Hanlde_GivenInvalidRequest_ShouldThrowPlayerIsNotAMemberOfTeam()
        {
            // Arrange
            var participantIds = new List<string> { "Foo1", "Foo2" };
            for (int i = 1; i <= 5; i++)
            {
                var currentPlayer = new Player { UserName = $"MatchParticipant{i}", Email = "tt@tt.tt", PasswordHash = "asdf", };
                this.dbContext.Players.Add(currentPlayer);
                this.dbContext.SaveChanges();

                this.dbContext.SaveChanges();

                participantIds.Add(currentPlayer.Id);
            }

            for (int i = 1; i <= 5; i++)
            {
                var currentPlayer = new Player { UserName = $"MatchParticipant{i + 5}", Email = "tt@tt.tt", PasswordHash = "asdf", };
                this.dbContext.Players.Add(currentPlayer);
                this.dbContext.SaveChanges();

                currentPlayer.PlayerTeams.Add(new PlayerTeam { PlayerId = currentPlayer.Id, TeamId = 2 });
                this.dbContext.SaveChanges();

                participantIds.Add(currentPlayer.Id);
            }

            var command = new EditMatchResultCommand { Id = 1, ParticipatedPlayersIds = participantIds, AwayTeamScore = 2, HomeTeamScore = 2 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new EditMatchResultCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<PlayerIsNotAMemberOfTeamException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
