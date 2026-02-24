using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Domain.Entities.Patients;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Patients;

public class UpdatePatientUseCaseTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
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

        _mapperMock = new Mock<IMapper>();
        _mapperMock
            .Setup(m => m.Map<Patient>(It.IsAny<CreatePatientRequest>()))
            .Returns(_patient);

        _useCase = new UpdatePatientUseCase(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task ShouldUpdatePatientCorrectly()
    {
        // Arrange
        Guid patientId = Guid.NewGuid();
        _patient.Id = patientId;

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

        // Instanciando o Response conforme o construtor da sua classe
        var expectedResponse = new PatientResponse(
            id: patientId,
            name: request.Name,
            email: request.Email,
            cpf: request.Cpf,
            phone: request.Phone,
            birthDate: request.BirthDate,
            notes: request.Notes,
            rg: request.Rg,
            gender: request.Gender,
            createdAt: DateTime.Now.AddDays(-1),
            updatedAt: DateTime.Now
        );

        // 1. Setup para encontrar o paciente no repositório
        _repositoryMock
            .Setup(r => r.GetById(patientId))
            .ReturnsAsync(_patient);

        // 2. Setup para o mapeamento de ATUALIZAÇÃO (Request -> Entidade Existente)
        // Importante: No seu UseCase você usa _mapper.Map(request, patient)
        _mapperMock
            .Setup(m => m.Map(request, _patient))
            .Returns(_patient);

        // 3. Setup para o mapeamento de RETORNO (Entidade -> PatientResponse)
        _mapperMock
            .Setup(m => m.Map<PatientResponse>(_patient))
            .Returns(expectedResponse);

        // Act
        var result = await _useCase.Execute(patientId, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(patientId, result.Id);
        Assert.Equal(request.Name, result.Name);
        
        _repositoryMock.Verify(r => r.GetById(patientId), Times.Once);
        _mapperMock.Verify(m => m.Map(request, _patient), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.Is<Patient>(p => p.Id == patientId)), Times.Once);
    }
}
