namespace BESL.Application.Tests.TeamTableResults.Commands.AddPoints
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
    using BESL.Application.TeamTableResults.Commands.AddPoints;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class AddPointsCommandTests : BaseTest<TeamTableResult>
    {
        [Trait(nameof(TeamTableResult), "AddPoints command tests")]
        [Fact(DisplayName = "Handle given valid request should add points")]
        public async Task Handle_GivenValidRequest_ShouldAddPoints()
        {
            // Arrange
            var command = new AddPointsCommand { Points = 5, TeamId = 1, TournamentId = 1 };
            var sut = new AddPointsCommandHandler(this.deletableEntityRepository);

            // Act
            var rowsAffected = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            rowsAffected.ShouldBe(1);

            var desiredTTR = this.deletableEntityRepository.AllAsNoTracking().OrderByDescending(x => x.ModifiedOn).FirstOrDefault();
            desiredTTR.Points.ShouldBe(5);
        }

        [Trait(nameof(TeamTableResult), "AddPoints command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddPointsCommandHandler(It.IsAny<IDeletableEntityRepository<TeamTableResult>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TeamTableResult), "AddPoints command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new AddPointsCommand { Points = 5, TeamId = 212, TournamentId = 112 };
            var sut = new AddPointsCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
