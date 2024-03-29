﻿namespace BESL.Application.Tests.Tournaments.Queries.Details
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
    using BESL.Application.Tournaments.Queries.Details;
    using BESL.Entities;

    public class GetTournamentDetailsQueryTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "GetTournqmentDetails query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model.")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetTournamentDetailsQuery { Id = 1 };
            var userAccessorMock = new Mock<IUserAccessor>();
            userAccessorMock.Setup(x => x.UserId).Returns("Foo1");

            var sut = new GetTournamentDetailsQueryHandler(this.deletableEntityRepository, this.mapper, userAccessorMock.Object);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetTournamentDetailsViewModel>();
            viewModel.IsCurrentUserInAnEnrolledTeam.ShouldBe(true);
            viewModel.Name.ShouldBe("TestTournament1");
        }

        [Trait(nameof(Tournament), "GetTournqmentDetails query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetTournamentDetailsQueryHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "GetTournqmentDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException.")]
        public async Task Handle_GivenNullRequest_ShouldReturnArgumentNullException()
        {
            // Arrange
            var query = new GetTournamentDetailsQuery { Id = 122 };
            var sut = new GetTournamentDetailsQueryHandler(this.deletableEntityRepository, It.IsAny<IMapper>(), It.IsAny<IUserAccessor>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
