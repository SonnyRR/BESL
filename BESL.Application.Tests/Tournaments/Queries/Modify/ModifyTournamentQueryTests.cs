namespace BESL.Application.Tests.Tournaments.Queries.Modify
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
    using BESL.Application.Tournaments.Queries.Modify;
    using BESL.Entities;

    public class ModifyTournamentQueryTests : BaseTest<Tournament>
    {
        [Trait(nameof(Tournament), "ModifyTournament query tests")]
        [Fact(DisplayName = "Handle given valid request should return ModifyTournamentCommand")]
        public async Task Handle_GivenValidRequest_ShouldReturnModifyTournamentCommand()
        {
            // Arrange
            var query = new ModifyTournamentQuery { Id = 1 };
            var sut = new ModifyTournamentQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.Name.ShouldBe("TestTournament1");            
        }

        [Trait(nameof(Tournament), "ModifyTournament query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ModifyTournamentQueryHandler(It.IsAny<IDeletableEntityRepository<Tournament>>(), It.IsAny<IMapper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Tournament), "ModifyTournament query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new ModifyTournamentQuery { Id = 123 };
            var sut = new ModifyTournamentQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }
    }
}
