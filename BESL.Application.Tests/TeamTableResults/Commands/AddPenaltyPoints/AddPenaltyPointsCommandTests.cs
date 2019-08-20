namespace BESL.Application.Tests.TeamTableResults.Commands.AddPenaltyPoints
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
    using BESL.Application.TeamTableResults.Commands.AddPenaltyPoints;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class AddPenaltyPointsCommandTests : BaseTest<TeamTableResult>
    {
        [Trait(nameof(TeamTableResult), "AddPenaltyPoints command tests")]
        [Fact(DisplayName = "Handle given valid request should add penalty points")]
        public async Task Handle_GivenValidRequest_ShouldAddPenaltyPoints()
        {
            // Arrange
            var command = new AddPenaltyPointsCommand { TeamTableResultId = 1, PenaltyPoints = 5 };
            var sut = new AddPenaltyPointsCommandHandler(this.deletableEntityRepository);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(1);

            var updatedEntity = this.deletableEntityRepository.AllAsNoTracking().SingleOrDefault(x => x.Id == 1);
            updatedEntity.PenaltyPoints.ShouldBe(5);
        }

        [Trait(nameof(TeamTableResult), "AddPenaltyPoints command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddPenaltyPointsCommandHandler(It.IsAny<IDeletableEntityRepository<TeamTableResult>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TeamTableResult), "AddPenaltyPoints command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new AddPenaltyPointsCommand { TeamTableResultId = 1124124, PenaltyPoints = It.IsAny<int>() };
            var sut = new AddPenaltyPointsCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
