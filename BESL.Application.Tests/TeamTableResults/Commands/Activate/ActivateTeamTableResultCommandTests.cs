namespace BESL.Application.Tests.TeamTableResults.Commands.Activate
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
    using BESL.Application.TeamTableResults.Commands.Activate;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;

    public class ActivateTeamTableResultCommandTests : BaseTest<TeamTableResult>
    {
        [Trait(nameof(TeamTableResult), "ActivateTeamTableResult command tests")]
        [Fact(DisplayName = "Handle given valid request should activate TTR")]
        public async Task Handle_GivenValidRequest_ShouldActivateTTR()
        {
            // Arrange
            var command = new ActivateTeamTableResultCommand { TeamTableResultId = 2 };
            var sut = new ActivateTeamTableResultCommandHandler(this.deletableEntityRepository);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(1);

            var ttrModified = this.deletableEntityRepository.AllAsNoTracking().SingleOrDefault(x => x.Id == 2);

            ttrModified.IsDropped.ShouldBe(false);
        }

        [Trait(nameof(TeamTableResult), "ActivateTeamTableResult command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ActivateTeamTableResultCommandHandler(It.IsAny<IDeletableEntityRepository<TeamTableResult>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TeamTableResult), "ActivateTeamTableResult command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new ActivateTeamTableResultCommand { TeamTableResultId = 21424 };
            var sut = new ActivateTeamTableResultCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
