namespace BESL.Application.Tests.TournamentTables.Queries.GetTournamentTables
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Shouldly;
    using System;
    using Xunit;

    using BESL.Application.Exceptions;
    using BESL.Application.Interfaces;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentTables.Queries.GetTournamentTables;
    using BESL.Entities;

    public class GetTournamentTablesQueryTests : BaseTest<TournamentTable>
    {
        [Trait(nameof(TournamentTable), "GetTournamentTables query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetTournamentTablesQuery { Id = 1 };
            var sut = new GetTournamentTablesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act 
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Tables.Count().ShouldBeGreaterThan(0);
            viewModel.TournamentId.ShouldBe(1);
        }

        [Trait(nameof(TournamentTable), "GetTournamentTables query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetTournamentTablesQueryHandler(It.IsAny<IDeletableEntityRepository<TournamentTable>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(TournamentTable), "GetTournamentTables query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetTournamentTablesQuery { Id = 24 };
            var sut = new GetTournamentTablesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
