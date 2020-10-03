namespace BESL.Application.Tests.Tournaments.Queries.GetAllTournaments
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
    using BESL.Application.Tournaments.Queries.GetAllTournaments;
    using BESL.Entities;

    public class GetAllTournamentsQueryTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "GetAllTournaments query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllTournamentsQuery();
            var sut = new GetAllTournamentsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<AllTournamentsViewModel>();
            viewModel.Tournaments.Count().ShouldBe(2);
        }

        [Trait(nameof(Tournament), "GetAllTournaments query tests")]
        [Fact(DisplayName ="Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange           
            var sut = new GetAllTournamentsQueryHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
