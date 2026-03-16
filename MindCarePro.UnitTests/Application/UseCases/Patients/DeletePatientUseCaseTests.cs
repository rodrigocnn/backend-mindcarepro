using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Domain.Entities.Patients;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Patients;

public class DeletePatientUseCaseTests

{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<ICurrentUser> _currentUserMock;
    private readonly  DeletePatientUseCase _useCase ;
    private readonly Patient _patient ;
    
    public DeletePatientUseCaseTests()
    {
  
        _patient = PatientFaker.Create();

        _repositoryMock = new Mock<IPatientRepository>();
        _repositoryMock
            .Setup(r => r.Add(It.IsAny<Patient>()))
            .Returns(Task.CompletedTask);

        _currentUserMock = new Mock<ICurrentUser>();
        _currentUserMock.SetupGet(c => c.UserId).Returns(Guid.NewGuid());
        
        _useCase = new DeletePatientUseCase(
            _repositoryMock.Object,
            _currentUserMock.Object
        );
    }
    
    
    [Fact]
    public async Task ShouldDeletePatientCorrectly()
    {
        
        Guid patientId = Guid.NewGuid();
        _patient.Id = patientId;
        var userId = _currentUserMock.Object.UserId!.Value;
        _patient.UpdateUser(userId);
        
        _repositoryMock
            .Setup(r => r.GetById(patientId, userId))
            .ReturnsAsync(_patient);
        
        // Act
        var result = await _useCase.Execute(_patient.Id);
        
        Assert.NotNull(result);
        Assert.Equal(patientId, result.Id);
        
        _repositoryMock.Verify(r => r.GetById(patientId, userId), Times.Once);
    }
}
