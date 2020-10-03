using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

using BESL.Application.Exceptions;
using BESL.Application.Interfaces;
using BESL.Application.Teams.Queries.Details;
using BESL.Application.Tests.Infrastructure;
using BESL.Entities;

namespace BESL.Application.Tests.Teams.Queries.Details
{
    public class GetTeamDetailsQueryTests : BaseTest<Team>
    {
        [Trait(nameof(Team), "GetTeamDetails query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetTeamDetailsQuery { Id = 1 };
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new GetTeamDetailsQueryHandler(this.deletableEntityRepository, this.mapper, userAccessorMock.Object);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Name.ShouldBe("FooTeam1");
            viewModel.IsOwner.ShouldBe(true);
            viewModel.IsMember.ShouldBe(true);
        }

        [Trait(nameof(Team), "GetTeamDetails query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetTeamDetailsQueryHandler(It.IsAny<IDeletableEntityRepository<Team>>(), It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Team), "GetTeamDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetTeamDetailsQuery { Id = 131 };
            var sut = new GetTeamDetailsQueryHandler(this.deletableEntityRepository, It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
