namespace BESL.Application.Tests.Search.Queries
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Search.Queries.QuerySearch;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Persistence.Repositories;

    public class SearchQueryTests : BaseTest<Player>
    {
        [Trait("Search", "Search query tests")]
        [Fact(DisplayName = "Handle given valid request should return valid player search result")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidPlayerSearchQueryResults()
        {
            // Arrange
            var query = new SearchQuery { Query = "FooP1" };

            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentsRepository = new EfDeletableEntityRepository<Tournament>(this.dbContext);

            var sut = new SearchQueryHandler(this.deletableEntityRepository, teamsRepository, tournamentsRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Arrange
            viewModel.ShouldNotBeNull();
            viewModel.Players.Count().ShouldBe(1);
        }

        [Trait("Search", "Search query tests")]
        [Fact(DisplayName = "Handle given valid request should return valid search result")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidSearchQueryResults()
        {
            // Arrange
            var query = new SearchQuery { Query = "Foo" };

            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentsRepository = new EfDeletableEntityRepository<Tournament>(this.dbContext);

            var sut = new SearchQueryHandler(this.deletableEntityRepository, teamsRepository, tournamentsRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Arrange
            viewModel.ShouldNotBeNull();
            viewModel.Players.Count().ShouldBeGreaterThan(2);
            viewModel.Teams.Count().ShouldBeGreaterThan(2);
        }

        [Trait("Search", "Search query tests")]
        [Fact(DisplayName = "Handle given valid request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new SearchQueryHandler(
                It.IsAny<IDeletableEntityRepository<Player>>(),
                It.IsAny<IDeletableEntityRepository<Team>>(),
                It.IsAny<IDeletableEntityRepository<Tournament>>(),
                It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait("Search", "Search query tests")]
        [Fact(DisplayName = "Handle given valid request should throw InvalidSearchQueryException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowInvalidSearchQueryException()
        {
            // Arrange
            var query = new SearchQuery { Query = string.Empty };

            var teamsRepository = new EfDeletableEntityRepository<Team>(this.dbContext);
            var tournamentsRepository = new EfDeletableEntityRepository<Tournament>(this.dbContext);

            var sut = new SearchQueryHandler(this.deletableEntityRepository, teamsRepository, tournamentsRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<InvalidSearchQueryException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
