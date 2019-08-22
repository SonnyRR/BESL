namespace BESL.Application.Tests.PlayWeeks.Command.Advance
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.PlayWeeks.Commands.Advance;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class AdvanceActiveTournamentsPlayWeeksCommandTests : BaseTest<PlayWeek>
    {
        [Trait(nameof(PlayWeek), "AdvanceActiveTournamentsPlayWeeks command tests")]
        [Fact(DisplayName = "Handle given valid request should advance to next play week")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var command = new AdvanceActiveTournamentsPlayWeeksCommand();
            var sut = new AdvanceActiveTournamentsPlayWeeksCommandHandler(this.deletableEntityRepository);

            // Act
            var affectedRows = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            affectedRows.ShouldBe(6);
        }

        [Trait(nameof(PlayWeek), "AdvanceActiveTournamentsPlayWeeks command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AdvanceActiveTournamentsPlayWeeksCommandHandler(It.IsAny<IDeletableEntityRepository<PlayWeek>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
