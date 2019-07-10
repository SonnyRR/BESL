namespace BESL.Application.Tests.TournamentFormats.Commands.Create
{
    using BESL.Application.Exceptions;
    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentFormats.Commands.Create;
    using BESL.Domain.Entities;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class CreateTournamentFormatCommandTests : BaseTest
    {
        [Trait(nameof(TournamentFormat), "TournamentFormat creation tests.")]
        [Fact(DisplayName = "Handle given valid request should create valid entity.")]
        public async Task Handle_GivenValidRequest_ShouldCreateValidEntity()
        {
            // Arrange
            var request = new CreateTournamentFormatCommand()
            {
                GameId = 2,
                Name = "5v5",
                Description = "Plain old 5v5 classic counter-strike competitie format",
                TeamPlayersCount = 5
            };

            var sut = new CreateTournamentFormatHandler(this.dbContext);

            // Act
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBe(1);
        }

        [Trait(nameof(TournamentFormat), "TournamentFormat creation tests.")]
        [Fact(DisplayName = "Handle given Invalid request should throw NotFoundException.")]
        public void sHandle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new CreateTournamentFormatCommand()
            {
                GameId = 90125,
                Name = "5v5",
                Description = "Plain old 5v5 classic counter-strike competitie format",
                TeamPlayersCount = 5
            };

            var sut = new CreateTournamentFormatHandler(this.dbContext);

            // Assert
            Should.Throw<NotFoundException>(sut.Handle(request, CancellationToken.None));
        }

        [Trait(nameof(TournamentFormat), "TournamentFormat creation tests.")]
        [Fact(DisplayName = "Validator given invalid request should not validate entity.")]
        public void Validator_GivenInvalidRequest_ShouldNotValidateEntity()
        {
            // Arrange
            var request = new CreateTournamentFormatCommand()
            {
                GameId = 2,
                Name = "6v",
                Description = "NotLongEnough",
                TeamPlayersCount = -33
            };

            var sut = new CreateTournamentFormatValidator();

            // Act
            var validationResult = sut.Validate(request);

            // Assert
            validationResult.IsValid.ShouldBe(false);
            validationResult.Errors.Count.ShouldBe(3);
        }
    }
}
