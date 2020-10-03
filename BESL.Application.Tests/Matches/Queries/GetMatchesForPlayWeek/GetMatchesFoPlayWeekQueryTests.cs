namespace BESL.Application.Tests.Matches.Queries.GetMatchesForPlayWeek
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
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.Matches.Queries.GetMatchesForPlayWeek;
    using BESL.Entities;
    using BESL.Persistence.Repositories;
    using Match = BESL.Entities.Match;

    public class GetMatchesFoPlayWeekQueryTests : BaseTest<PlayWeek>
    {
        [Trait(nameof(Match), "GetMatchesForPlayWeek query tests")]
        [Fact(DisplayName ="Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetMatchesForPlayWeekQuery { PlayWeekId = 1 };

            var matchesRepository = new EfDeletableEntityRepository<Match>(this.dbContext);

            var sut = new GetMatchesForPlayWeekQueryHandler(this.deletableEntityRepository, matchesRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Matches.Count().ShouldBe(2);
            viewModel.WeekAsString.ShouldBe("11/08/2019 - 18/08/2019");
        }

        [Trait(nameof(Match), "GetMatchesForPlayWeek query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetMatchesForPlayWeekQueryHandler(It.IsAny<IDeletableEntityRepository<PlayWeek>>(), It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Match), "GetMatchesForPlayWeek query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetMatchesForPlayWeekQuery { PlayWeekId = 121 };
            var sut = new GetMatchesForPlayWeekQueryHandler(this.deletableEntityRepository, It.IsAny<IDeletableEntityRepository<Match>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
