namespace BESL.Application.Tests.TournamentFormats.Queries.GetAllTournamentFormats
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Application.TournamentFormats.Queries.GetAllTournamentFormats;

    public class GetAllTournamentFormatsTests : BaseTest<TournamentFormat>
    {
        [Trait(nameof(TournamentFormat), "GetAllTournamentFormats query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllTournamentFormatsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TournamentFormat), "GetAllTournamentFormats query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllTournamentFormatsQuery();
            var sut = new GetAllTournamentFormatsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.TournamentFormats.Count().ShouldBe(2);
        }
    }
}
