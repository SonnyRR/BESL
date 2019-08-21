namespace BESL.Application.Tests.Players.Queries.Details
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
    using BESL.Application.Players.Queries.Details;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;

    public class PlayerDetailsQueryTests : BaseTest<Player>
    {
        [Trait(nameof(Player), "PlayerDetails query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetPlayerDetailsQuery { Username = "FooP1" };
            var sut = new GetPlayerDetailsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Username.ShouldBe("FooP1");
            viewModel.Id.ShouldBe("Foo1");
        }

        [Trait(nameof(Player), "PlayerDetails query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetPlayerDetailsQueryHandler(It.IsAny<IDeletableEntityRepository<Player>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Player), "PlayerDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var query = new GetPlayerDetailsQuery { Username = "InvalidUsername" };
            var sut = new GetPlayerDetailsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
