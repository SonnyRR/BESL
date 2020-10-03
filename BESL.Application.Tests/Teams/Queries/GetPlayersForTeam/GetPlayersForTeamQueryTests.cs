namespace BESL.Application.Tests.Teams.Queries.GetPlayersForTeam
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Queries.GetPlayersForTeam;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;

    public class GetPlayersForTeamQueryTests : BaseTest<PlayerTeam>
    {
        [Trait(nameof(Team), "GetPlayersForTeam query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetPlayersForTeamQuery { TeamId = 1 };
            var sut = new GetPlayersForTeamQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Players.ShouldNotBeEmpty();
        }

        [Trait(nameof(Team), "GetPlayersForTeam query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetPlayersForTeamQueryHandler(It.IsAny<IDeletableEntityRepository<PlayerTeam>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
