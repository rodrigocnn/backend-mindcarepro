using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Domain.Entities.Psychologists;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Psychologists;

public class DeletePsychologistUseCaseTests
{
    private readonly Mock<IPsychologistRepository> _repositoryMock;
    private readonly Mock<ICurrentUser> _currentUserMock;
    private readonly DeletePsychologistUseCase _useCase;
    private readonly Psychologist _psychologist;

    public DeletePsychologistUseCaseTests()
    {
        _psychologist = PsychologistFaker.Create();

        _repositoryMock = new Mock<IPsychologistRepository>();

        _currentUserMock = new Mock<ICurrentUser>();
        _currentUserMock.SetupGet(c => c.UserId).Returns(Guid.NewGuid());

        _useCase = new DeletePsychologistUseCase(
            _repositoryMock.Object,
            _currentUserMock.Object
        );
    }

    [Fact]
    public async Task ShouldDeletePsychologistCorrectly()
    {
        // Arrange
        Guid psychologistId = Guid.NewGuid();
        _psychologist.Id = psychologistId;
        _currentUserMock.SetupGet(c => c.UserId).Returns(psychologistId);
        var userId = psychologistId;

        _repositoryMock
            .Setup(r => r.GetById(psychologistId, userId))
            .ReturnsAsync(_psychologist);

        _repositoryMock
            .Setup(r => r.Update(It.IsAny<Psychologist>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Execute(psychologistId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(psychologistId, result.Value!.Id);

        _repositoryMock.Verify(r => r.GetById(psychologistId, userId), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.Is<Psychologist>(p => p.Id == psychologistId)), Times.Once);
    }
}
