using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Domain.Entities.Patients;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Patients;

public class CreatePatientUseCaseTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IValidationService> _validationMock;
    private readonly Mock<ICurrentUser> _currentUserMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreatePatientUseCase _useCase;
    private readonly Patient _patient;

    public CreatePatientUseCaseTests()
    {
  
        _patient = PatientFaker.Create();

        _repositoryMock = new Mock<IPatientRepository>();
        _repositoryMock
            .Setup(r => r.Add(It.IsAny<Patient>()))
            .Returns(Task.CompletedTask);

        _validationMock = new Mock<IValidationService>();
        _validationMock
            .Setup(v => v.ValidateAsync(It.IsAny<CreatePatientRequest>()))
            .Returns(Task.CompletedTask);

        _mapperMock = new Mock<IMapper>();
        _mapperMock
            .Setup(m => m.Map<Patient>(It.IsAny<CreatePatientRequest>()))
            .Returns(_patient);
        
        _currentUserMock= new Mock<ICurrentUser>();
        _currentUserMock.SetupGet(c => c.UserId).Returns(Guid.NewGuid());

        _useCase = new CreatePatientUseCase(
            _repositoryMock.Object,
            _validationMock.Object,
            _currentUserMock.Object,
            _mapperMock.Object
     
        );
    }

    public CreatePatientUseCaseTests(Mock<ICurrentUser> currentUserMock)
    {
        _currentUserMock = currentUserMock;
    }

    [Fact]
    public async Task ShouldCreatePatientCorrectly()
    {
        var request = new CreatePatientRequest(
            _patient.Name,
            _patient.Email,
            _patient.Cpf,
            _patient.Phone,
            _patient.BirthDate,
            _patient.Notes,
            _patient.Rg,
            _patient.Gender
        );

        // Act
        var result = await _useCase.Execute(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_patient.Name, result.Name);
        Assert.Equal(_patient.Cpf, result.Cpf);

        _validationMock.Verify(v => v.ValidateAsync(request), Times.Once);
        _mapperMock.Verify(m => m.Map<Patient>(request), Times.Once);
        _repositoryMock.Verify(r => r.Add(It.Is<Patient>(p => p == _patient)), Times.Once);
    }
}
