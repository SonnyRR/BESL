namespace BESL.Application.Tests.Teams.Queries.TransferOwnership
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.TransferOwnership;
    using BESL.Application.Teams.Queries.TransferOwnership;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;

    public class TransformOwnershipQueryTests : BaseTest<PlayerTeam>
    {
        [Trait(nameof(Team), "TransferTeamOwnership query tests")]
        [Fact(DisplayName = "Handle given valid request should return TransferOwnershipCommand")]
        public async Task Handle_GivenValidRequest_ShouldReturnTransferOwnershipCommand()
        {
            // Arrange
            var query = new TransferTeamOwnershipQuery { TeamId = 1 };
            var sut = new TransferTeamOwnershipQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeOfType<TransferTeamOwnershipCommand>();
            command.TeamPlayers.ShouldNotBeEmpty();
        }

        [Trait(nameof(Team), "TransferTeamOwnership query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new TransferTeamOwnershipQueryHandler(It.IsAny<IDeletableEntityRepository<PlayerTeam>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
