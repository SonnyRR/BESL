namespace BESL.Application.Tests.Matches.Commands.Create
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
    using BESL.Application.Matches.Commands.Create;
    using BESL.Domain.Entities;
    using BESL.Persistence.Repositories;
    using Match = Domain.Entities.Match;

    public class CreateMatchCommandTests : BaseTest<Match>
    {
        [Trait(nameof(Player), "CreateMatch command tests")]
        [Fact(DisplayName = "Handle given valid request should create match")]
        public async Task Handle_GivenValidRequest_ShouldCreateMatch()
        {
            // Arrange
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var playWeeksRepository = new EfDeletableEntityRepository<PlayWeek>(this.dbContext);

            var command = new CreateMatchCommand { HomeTeamId = 1, AwayTeamId = 5, PlayWeekId = 1, PlayDate = new DateTime(2019, 08, 15), TournamentTableId = 1 };
            var sut = new CreateMatchCommandHandler(this.deletableEntityRepository, teamsRepository, playWeeksRepository);

            // Act
            var matchId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            matchId.ShouldBeGreaterThan(0);

            var createdMatch = this.dbContext.Matches.SingleOrDefault(x => x.ScheduledDate == new DateTime(2019, 08, 15));
            createdMatch.HomeTeamId.ShouldBe(1);

        }

        [Trait(nameof(Player), "CreateMatch command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateMatchCommandHandler(
                It.IsAny<IDeletableEntityRepository<Match>>(),
                It.IsAny<IDeletableEntityRepository<Team>>(),
                It.IsAny<IDeletableEntityRepository<PlayWeek>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "CreateMatch command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException(HomeTeam)")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowHomeTeamNotFoundException()
        {
            // Arrange
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var playWeeksRepository = new EfDeletableEntityRepository<PlayWeek>(this.dbContext);

            var command = new CreateMatchCommand { HomeTeamId = 112, AwayTeamId = 2, PlayWeekId = 1, PlayDate = new DateTime(2019, 08, 15), TournamentTableId = 1 };
            var sut = new CreateMatchCommandHandler(this.deletableEntityRepository, teamsRepository, playWeeksRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "CreateMatch command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException(AwayTeam)")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowAwayTeamNotFoundException()
        {
            // Arrange
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var playWeeksRepository = new EfDeletableEntityRepository<PlayWeek>(this.dbContext);

            var command = new CreateMatchCommand { HomeTeamId = 1, AwayTeamId = 222, PlayWeekId = 1, PlayDate = new DateTime(2019, 08, 15), TournamentTableId = 1 };
            var sut = new CreateMatchCommandHandler(this.deletableEntityRepository, teamsRepository, playWeeksRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "CreateMatch command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ForbiddenException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowForbiddenException()
        {
            // Arrange
            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var playWeeksRepository = new EfDeletableEntityRepository<PlayWeek>(this.dbContext);

            var command = new CreateMatchCommand { HomeTeamId = 1, AwayTeamId = 4, PlayWeekId = 1, PlayDate = new DateTime(2019, 08, 15), TournamentTableId = 1 };
            var sut = new CreateMatchCommandHandler(this.deletableEntityRepository, teamsRepository, playWeeksRepository);

            // Act & Assert
            await Should.ThrowAsync<ForbiddenException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
