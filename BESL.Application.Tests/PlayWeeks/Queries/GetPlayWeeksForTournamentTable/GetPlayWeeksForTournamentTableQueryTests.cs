namespace BESL.Application.Tests.PlayWeeks.Queries.GetPlayWeeksForTournamentTable
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
    using BESL.Application.PlayWeeks.Queries.GetPlayWeeksForTournamentTable;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Entities;
    using BESL.Persistence.Repositories;

    public class GetPlayWeeksForTournamentTableQueryTests : BaseTest<PlayWeek>
    {
        [Trait(nameof(PlayWeek), "GetPlayWeeksForTournamentTable query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetPlayWeeksForTournamentTableQuery { TournamentTableId = 1 };

            var tournamentsRepository = new EfDeletableEntityRepository<TournamentTable>(this.dbContext);

            var sut = new GetPlayWeeksForTournamentTableQueryHandler(this.deletableEntityRepository, tournamentsRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.PlayWeeks.Count().ShouldBe(3);
        }

        [Trait(nameof(PlayWeek), "GetPlayWeeksForTournamentTable query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetPlayWeeksForTournamentTableQueryHandler(
                It.IsAny<IDeletableEntityRepository<PlayWeek>>(),
                It.IsAny<IDeletableEntityRepository<TournamentTable>>(),
                It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(PlayWeek), "GetPlayWeeksForTournamentTable query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetPlayWeeksForTournamentTableQuery { TournamentTableId = 121 };

            var tournamentsRepository = new EfDeletableEntityRepository<TournamentTable>(this.dbContext);

            var sut = new GetPlayWeeksForTournamentTableQueryHandler(this.deletableEntityRepository, tournamentsRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
