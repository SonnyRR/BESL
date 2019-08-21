namespace BESL.Application.Tests.Players.Queries.GetMatchParticipatedPlayers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.Players.Queries.GetMatchParticipatedPlayers;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using Match = Domain.Entities.Match;

    public class GetMatchParticipatedPlayersQueryTests : BaseTest<Match>
    {
        [Trait(nameof(Player), "GetMatchParticipatedPlayers query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetMatchParticipatedPlayersQuery { MatchId = 1 };
            var sut = new GetMatchParticipatedPlayersQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Players.Count().ShouldBe(2);
        }

        [Trait(nameof(Player), "GetMatchParticipatedPlayers query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetMatchParticipatedPlayersQueryHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
