namespace BESL.Application.Tests.TournamentFormats.Queries.GetAllTournamentFormatsSelectList
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Xunit;
    using Shouldly;

    using BESL.Application.TournamentFormats.Queries.GetAllTournamentFormatsSelectList;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Common.Models.Lookups;
    using BESL.Domain.Entities;

    public class GetAllTournamentFormatsSelectListTests : BaseTest<TournamentFormat>
    {
        [Trait(nameof(TournamentFormat), "GetAllTournamentFormatsSelectList query tests.")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllTournamentFormatsSelectListQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TournamentFormat), "GetAllTournamentFormatsSelectList query tests.")]
        [Fact(DisplayName = "Handle given valid request should return IEnumerable.")]
        public async Task Handle_GivenValidRequest_ShouldReturnIEnumerable()
        {
            // Arrange
            var query = new GetAllTournamentFormatsSelectListQuery();
            var sut = new GetAllTournamentFormatsSelectListQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeAssignableTo<IEnumerable<TournamentFormatSelectItemLookupModel>>();
            viewModel.Count().ShouldBe(2);

        }
    }
}
