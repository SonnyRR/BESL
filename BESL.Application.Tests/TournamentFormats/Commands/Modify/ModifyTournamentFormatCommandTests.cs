namespace BESL.Application.Tests.TournamentFormats.Commands.Modify
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    using Xunit;
    using Moq;
    using Shouldly;

    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Application.TournamentFormats.Commands.Modify;

    public class ModifyTournamentFormatCommandTests : BaseTest<TournamentFormat>
    {
        [Trait(nameof(TournamentFormat), "ModifyTournamentFormat command tests.")]
        [Fact(DisplayName = "Handle given valid request should delete valid entity.")]
        public async Task Handle_GivenValidRequest_ShouldModifyEntity()
        {
            // Arrange            
            var sut = new ModifyTournamentFormatCommandHandler(deletableEntityRepository);

            var tournamentFormatId = 2;
            var command = new ModifyTournamentFormatCommand
            {
                Id = tournamentFormatId,
                Name = "9v9",
                Description = "Changed description from test",
            };

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var desiredEntity = this.deletableEntityRepository.AllAsNoTracking().SingleOrDefault(x => x.Id == tournamentFormatId);

            id.ShouldBe(tournamentFormatId);
            desiredEntity.Name.ShouldBe("9v9");
            desiredEntity.Description.ShouldBe("Changed description from test");
        }

        [Trait(nameof(TournamentFormat), "ModifyTournamentFormat command tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arange
            var sut = new ModifyTournamentFormatCommandHandler(deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
