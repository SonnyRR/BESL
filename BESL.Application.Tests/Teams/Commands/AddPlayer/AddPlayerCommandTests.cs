namespace BESL.Application.Tests.Teams.Commands.AddPlayer
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
    using BESL.Application.Teams.Commands.AddPlayer;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class AddPlayerCommandTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "AddPlayer command tests")]
        [Fact(DisplayName = "Handle given valid request should add player to team")]
        public async Task Handle_GivenValidRequest_ShouldAddPlayerToTeam()
        {
            // Arrange
            var command = new AddPlayerCommand { TeamId = 1, PlayerId = "Foo5" };
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);
            var sut = new AddPlayerCommandHandler(this.deletableEntityRepository, playerTeamsRepository);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBeGreaterThan(0);

            var isPlayerPresent = playerTeamsRepository.AllAsNoTracking().Any(x => x.PlayerId == "Foo5" && x.TeamId == 1);
            isPlayerPresent.ShouldBe(true);
        }

        [Trait(nameof(Team), "AddPlayer command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddPlayerCommandHandler(It.IsAny<IDeletableEntityRepository<Team>>(), It.IsAny<IDeletableEntityRepository<PlayerTeam>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "AddPlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new AddPlayerCommand { TeamId = 313, PlayerId = "Foo5" };
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);
            var sut = new AddPlayerCommandHandler(this.deletableEntityRepository, playerTeamsRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "AddPlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw TeamIsFullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowTeamIsFullException()
        {
            // Arrange
            var command = new AddPlayerCommand { TeamId = 1, PlayerId = "Foo5" };
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);
            var sut = new AddPlayerCommandHandler(this.deletableEntityRepository, playerTeamsRepository);

            for (int i = 0; i < 10; i++)
            {
                var player = new Player { UserName = $"Placeholder{i}", Email = "foomail.bg", };
                this.dbContext.Add(player);
                this.dbContext.PlayerTeams.Add(new PlayerTeam { TeamId = 1, PlayerId = player.Id });
                this.dbContext.SaveChanges();
            }


            // Act & Assert
            await Should.ThrowAsync<TeamIsFullException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "AddPlayer command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowPlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException()
        {
            // Arrange
            var command = new AddPlayerCommand { TeamId = 1, PlayerId = "Foo2" };
            var playerTeamsRepository = new EfDeletableEntityRepository<PlayerTeam>(this.dbContext);
            var sut = new AddPlayerCommandHandler(this.deletableEntityRepository, playerTeamsRepository);

            // Act & Assert
            await Should.ThrowAsync<PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
