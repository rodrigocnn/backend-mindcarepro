using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Domain.Entities.Psychologists;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Psychologists;

public class DeletePsychologistUseCaseTests
{
    private readonly Mock<IPsychologistRepository> _repositoryMock;
    private readonly DeletePsychologistUseCase _useCase;
    private readonly Psychologist _psychologist;

    public DeletePsychologistUseCaseTests()
    {
        _psychologist = PsychologistFaker.Create();

        _repositoryMock = new Mock<IPsychologistRepository>();

        _useCase = new DeletePsychologistUseCase(
            _repositoryMock.Object
        );
    }

    [Fact]
    public async Task ShouldDeletePsychologistCorrectly()
    {
        // Arrange
        Guid psychologistId = Guid.NewGuid();
        _psychologist.Id = psychologistId;

        _repositoryMock
            .Setup(r => r.GetById(psychologistId))
            .ReturnsAsync(_psychologist);

        _repositoryMock
            .Setup(r => r.Update(It.IsAny<Psychologist>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Execute(psychologistId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(psychologistId, result.Id);

        _repositoryMock.Verify(r => r.GetById(psychologistId), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.Is<Psychologist>(p => p.Id == psychologistId)), Times.Once);
    }
}