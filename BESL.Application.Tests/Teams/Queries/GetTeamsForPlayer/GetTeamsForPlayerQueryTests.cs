namespace BESL.Application.Tests.Teams.Queries.GetTeamsForPlayer
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Queries.GetTeamsForPlayer;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class GetTeamsForPlayerQueryTests : BaseTest<PlayerTeam>
    {
        [Trait(nameof(Team), "GetTeamsForPlayer query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetTeamsForPlayerQuery { UserId = "Foo1", WithDeleted = false };
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new GetTeamsForPlayerQueryHandler(this.mapper, this.deletableEntityRepository, playersRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.PlayerTeams.ShouldNotBeEmpty();
        }

        [Trait(nameof(Team), "GetTeamsForPlayer query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetTeamsForPlayerQueryHandler(It.IsAny<IMapper>(), It.IsAny<IDeletableEntityRepository<PlayerTeam>>(), It.IsAny<IDeletableEntityRepository<Player>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "GetTeamsForPlayer query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetTeamsForPlayerQuery { UserId = "InvalidId", WithDeleted = false };
            var playersRepository = new EfDeletableEntityRepository<Player>(this.dbContext);

            var sut = new GetTeamsForPlayerQueryHandler(this.mapper, this.deletableEntityRepository, playersRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
