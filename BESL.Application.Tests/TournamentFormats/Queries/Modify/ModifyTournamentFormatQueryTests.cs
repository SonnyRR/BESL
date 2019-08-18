namespace BESL.Application.Tests.TournamentFormats.Queries.Modify
{
    using BESL.Application.Exceptions;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentFormats.Queries.Modify;
    using BESL.Domain.Entities;
    using Moq;
    using Shouldly;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class ModifyTournamentFormatQueryTests : BaseTest<TournamentFormat>
    {
        [Trait(nameof(TournamentFormat), "ModifyTournamentFormat query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyTournamentFormatQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TournamentFormat), "ModifyTournamentFormat query tests.")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new ModifyTournamentFormatQuery { Id = 21413 };
            var sut = new ModifyTournamentFormatQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TournamentFormat), "ModifyTournamentFormat query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidCommand()
        {
            // Arrange
            var query = new ModifyTournamentFormatQuery { Id = 2 };
            var sut = new ModifyTournamentFormatQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.Name.ShouldNotBeNullOrWhiteSpace();
            command.GameName.ShouldNotBeNullOrWhiteSpace();
            command.Description.ShouldNotBeNullOrWhiteSpace();
            command.Id.ShouldBe(2);
        }
    }
}
