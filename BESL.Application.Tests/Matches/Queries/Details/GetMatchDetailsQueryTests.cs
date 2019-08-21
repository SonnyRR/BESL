namespace BESL.Application.Tests.Matches.Queries.Details
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
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Queries.Details;
    using BESL.Domain.Entities;
    using Match = Domain.Entities.Match;

    public class GetMatchDetailsQueryTests : BaseTest<Match>
    {
        [Trait(nameof(Match), "GetMatchDetails query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetMatchDetailsQuery { Id = 1 };

            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new GetMatchDetailsQueryHandler(this.deletableEntityRepository, this.mapper, userAccessorMock.Object);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.HomeTeam.IsOwner.ShouldBe(true);
            viewModel.AwayTeam.IsOwner.ShouldBe(false);
            viewModel.HomeTeam.Name.ShouldBe("FooTeam1");
            viewModel.AwayTeam.Name.ShouldBe("FooTeam2");
            viewModel.HomeTeamScore.ShouldBe(2);
            viewModel.AwayTeamScore.ShouldBe(1);
        }

        [Trait(nameof(Match), "GetMatchDetails query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetMatchDetailsQueryHandler(It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "GetMatchDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetMatchDetailsQuery { Id = 123123 };

            var sut = new GetMatchDetailsQueryHandler(this.deletableEntityRepository, It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
