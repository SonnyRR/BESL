namespace BESL.Application.Tests.Teams.Queries.GetAllTeamsPaged
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Teams.Queries.GetAllTeamsPaged;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class GetAllTeamsPagedQueryTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "GetAllTeamsPaged query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllTeamsPagedQuery { Page = 0, PageSize = 3 };
            var sut = new GetAllTeamsPagedQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Teams.Count().ShouldBe(3);
        }

        [Trait(nameof(Team), "GetAllTeamsPaged query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllTeamsPagedQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
