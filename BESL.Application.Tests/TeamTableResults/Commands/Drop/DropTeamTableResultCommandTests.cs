namespace BESL.Application.Tests.TeamTableResults.Commands.Drop
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.TeamTableResults.Commands.Drop;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using System.Linq;

    public class DropTeamTableResultCommandTests : BaseTest<TeamTableResult>
    {
        [Trait(nameof(TeamTableResult), "DropTeamTableResult command tests")]
        [Fact(DisplayName = "Handle given valid request should drop team")]
        public async Task Handle_GivenValidRequest_ShouldDropTeam()
        {
            // Arrange
            var command = new DropTeamTableResultCommand { TeamTableResultId = 1 };
            var sut = new DropTeamTableResultCommandHandler(this.deletableEntityRepository);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(1);

            var updatedEntity = this.deletableEntityRepository.AllAsNoTracking().SingleOrDefault(x => x.Id == 1);
            updatedEntity.IsDropped.ShouldBe(true);
        }

        [Trait(nameof(TeamTableResult), "DropTeamTableResult command tests")]
        [Fact(DisplayName = "Handle given null request throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DropTeamTableResultCommandHandler(It.IsAny<IDeletableEntityRepository<TeamTableResult>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TeamTableResult), "DropTeamTableResult command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ThrowNotFoundException()
        {
            // Arrange
            var command = new DropTeamTableResultCommand { TeamTableResultId = 1134 };
            var sut = new DropTeamTableResultCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}
