
namespace BESL.Application.Tests.TournamentFormats.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;

    using Shouldly;
    using Xunit;

    using BESL.Application.Tests.Infrastructure;
    using BESL.Application.TournamentFormats.Commands.Delete;
    using BESL.Domain.Entities;
    using Moq;
    using BESL.Application.Exceptions;

    public class DeleteTournamentFormatCommandTests : BaseTest<TournamentFormat>
    {
        [Trait(nameof(TournamentFormat), "TournamentFormat deletion tests.")]
        [Fact(DisplayName = "Handle given valid request should delete valid entity.")]
        public async Task Handle_GivenValidRequest_ShouldCreateValidEntity()
        {
            // Arrange
            var request = new DeleteTournamentFormatCommand
            {
                Id = 2
            };

            var sut = new DeleteTournamentFormatCommandHandler(base.deletableEntityRepository);

            // Act
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBe(2);
        }

        [Trait(nameof(TournamentFormat), "TournamentFormat deletion tests.")]
        [Fact(DisplayName = "Handle given invalid request should throw DeleteFailureException.")]
        public async Task Handle_GivenInvalidRequest_ShouldThroDeleteFailureException()
        {
            // Arrange
            var request = new DeleteTournamentFormatCommand
            {
                Id = 3
            };

            var sut = new DeleteTournamentFormatCommandHandler(base.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<DeleteFailureException>(sut.Handle(request, CancellationToken.None));
        }
    }
}
