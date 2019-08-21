namespace BESL.Application.Tests.Games.Queries.GetAllGames
{
    using System;
    using System.Threading;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using Shouldly;
    using Xunit;
    using Moq;
    using MockQueryable.Moq;

    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Domain.Entities;
    using BESL.Application.Interfaces;

    public class GetAllGamesQueryTests
    {
        private readonly IMapper mapper;

        public GetAllGamesQueryTests()
        {
            this.mapper = AutoMapperFactory.Create();
        }

        [Trait(nameof(Game), "GetAllGames query tests.")]
        [Fact(DisplayName = "GetAllGamesQuery handler given valid request should return valid GetAllGames viewmodel.")]
        public async Task Handle_GivenValidRequest_ShouldReturnValidViewModel()
        {
            // Arrange
            var query = new GetAllGamesQuery();
            var gameRepositoryMock = new Mock<IDeletableEntityRepository<Game>>();
            var dataSet = new List<Game>()
            {
                new Game()
                {
                    Id = 1,
                    Name = It.IsAny<string>(),
                    Description = "SampleDesc",
                    CreatedOn = It.IsAny<DateTime>(),
                    GameImageUrl = It.IsAny<string>(),
                    TournamentFormats = new HashSet<TournamentFormat>()
                    {
                        new TournamentFormat()
                        {
                            Name = It.IsAny<string>(),
                            Teams = new HashSet<Team>()
                            {
                                new Team()
                                {
                                    Name = "SampleTeam"
                                }
                            }
                        }
                    }
                },
            }.AsQueryable();

            var dataSetMock = dataSet.BuildMock();
            gameRepositoryMock.Setup(m => m.AllAsNoTracking()).Returns(dataSetMock.Object);

            var sut = new GetAllGamesQueryHandler(gameRepositoryMock.Object, this.mapper);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            result.Games.Count.ShouldBe(1);
            result.Games.Any(x => x.RegisteredTeams == 1).ShouldBeTrue();
        }

        [Trait(nameof(Game), "GetAllGames query tests.")]
        [Fact(DisplayName = "GetAllGamesQuery handler given null request should throw ArgumentNullException.")]
        public async Task Handle_GivenNullRequest_ShouldReturnValidViewModel()
        {
            // Act
            var sut = new GetAllGamesQueryHandler(It.IsAny<IDeletableEntityRepository<Game>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}