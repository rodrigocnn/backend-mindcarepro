using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Domain.Entities.Patients;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Patients;

public class UpdatePatientUseCaseTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IValidationService> _validationMock;
    private readonly Mock<ICurrentUser> _currentUserMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdatePatientUseCase _useCase;
    private readonly Patient _patient;

    public UpdatePatientUseCaseTests()
    {
        _patient = PatientFaker.Create();

        _repositoryMock = new Mock<IPatientRepository>();
        _repositoryMock
            .Setup(r => r.Update(It.IsAny<Patient>()))
            .Returns(Task.CompletedTask);

        _validationMock = new Mock<IValidationService>();
        _validationMock
            .Setup(v => v.ValidateAsync(It.IsAny<CreatePatientRequest>()))
            .Returns(Task.CompletedTask);

        _currentUserMock = new Mock<ICurrentUser>();
        _currentUserMock.SetupGet(c => c.UserId).Returns(Guid.NewGuid());

        _mapperMock = new Mock<IMapper>();
        _mapperMock
            .Setup(m => m.Map<Patient>(It.IsAny<CreatePatientRequest>()))
            .Returns(_patient);

        _useCase = new UpdatePatientUseCase(
            _repositoryMock.Object,
            _validationMock.Object,
            _currentUserMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task ShouldUpdatePatientCorrectly()
    {
        // Arrange
        Guid patientId = Guid.NewGuid();
        _patient.Id = patientId;
        var userId = _currentUserMock.Object.UserId!.Value;
        _patient.UpdateUser(userId);

        var request = new CreatePatientRequest(
            _patient.Name + " Updated",
            _patient.Email,
            _patient.Cpf,
            _patient.Phone,
            _patient.BirthDate,
            _patient.Notes,
            _patient.Rg,
            _patient.Gender
        );

        // 1. Setup para encontrar o paciente no repositório
        _repositoryMock
            .Setup(r => r.GetById(patientId, userId))
            .ReturnsAsync(_patient);

        // 2. Setup para o mapeamento de ATUALIZAÇÃO (Request -> Entidade Existente)
        // Importante: No seu UseCase você usa _mapper.Map(request, patient)
        _mapperMock
            .Setup(m => m.Map(request, _patient))
            .Returns(_patient);

        // Act
        var result = await _useCase.Execute(patientId, request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(patientId, result.Value!.Id);
        Assert.Equal(request.Name, result.Value!.Name);
        
        _validationMock.Verify(v => v.ValidateAsync(request), Times.Once);
        _repositoryMock.Verify(r => r.GetById(patientId, userId), Times.Once);
        _mapperMock.Verify(m => m.Map(request, _patient), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.Is<Patient>(p => p.Id == patientId)), Times.Once);
    }
}
