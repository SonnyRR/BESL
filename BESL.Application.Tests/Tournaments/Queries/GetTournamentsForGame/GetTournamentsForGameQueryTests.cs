namespace BESL.Application.Tests.Tournaments.Queries.GetTournamentsForGame
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
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Tournaments.Queries.GetTournamentsForGame;
    using BESL.Domain.Entities;

    public class GetTournamentsForGameQueryTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "GetTournamentsForGame query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetTournamentsForGameQuery { GameId = 2 };
            var sut = new GetTournamentsForGameQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Tournaments.Count().ShouldBeGreaterThan(0);
        }

        [Trait(nameof(Tournament), "GetTournamentsForGame query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetTournamentsForGameQueryHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
