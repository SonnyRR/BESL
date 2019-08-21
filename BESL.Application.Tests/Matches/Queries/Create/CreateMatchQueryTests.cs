namespace BESL.Application.Tests.Matches.Queries.Create
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
    using BESL.Application.Matches.Queries.Create;
    using BESL.Domain.Entities;
    using BESL.Persistence.Repositories;
    using Match = Domain.Entities.Match;

    public class CreateMatchQueryTests : BaseTest<TournamentTable>
    {
        [Trait(nameof(Match), "CreateMatch query tests")]
        [Fact(DisplayName = "Handle given valid request should return create command")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new CreateMatchQuery { PlayWeekId = 1, TournamentTableId = 1 };

            var playWeeksRepository = new EfDeletableEntityRepository<PlayWeek>(this.dbContext);

            var sut = new CreateMatchQueryHandler(this.deletableEntityRepository, playWeeksRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.Teams.Count().ShouldBe(3);
        }

        [Trait(nameof(Match), "CreateMatch query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateMatchQueryHandler(It.IsAny<IDeletableEntityRepository<TournamentTable>>(), It.IsAny<IDeletableEntityRepository<PlayWeek>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
