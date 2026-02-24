using AutoMapper;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Domain.Entities.Psychologists;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Psychologists;

public class UpdatePsychologistUseCaseTests
{
    private readonly Mock<IPsychologistRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdatePsychologistUseCase _useCase;
    private readonly Psychologist _psychologist;

    public UpdatePsychologistUseCaseTests()
    {
        _psychologist = PsychologistFaker.Create();

        _repositoryMock = new Mock<IPsychologistRepository>();
        _repositoryMock
            .Setup(r => r.Update(It.IsAny<Psychologist>()))
            .Returns(Task.CompletedTask);

        _mapperMock = new Mock<IMapper>();

        _useCase = new UpdatePsychologistUseCase(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task ShouldUpdatePsychologistCorrectly()
    {
        // Arrange
        Guid psychologistId = Guid.NewGuid();
        _psychologist.Id = psychologistId;

        var request = new UpdatePsychologistRequest(
            name: _psychologist.Name + " Updated",
            email: _psychologist.Email,
            cpf: _psychologist.Cpf,
            phone: _psychologist.Phone,
            birthDate: _psychologist.Birth,
            rg: _psychologist.Rg,
            crp: _psychologist.Crp
        );

        var expectedResponse = new PsychologistResponse(
            id: psychologistId,
            name: request.Name,
            birth: request.BirthDate,
            email: request.Email,
            cpf: request.Cpf,
            rg: request.Rg,
            phone: request.Phone,
            crp: request.Crp,
            createdAt: _psychologist.CreatedAt,
            updatedAt: DateTime.Now,
            deletedAt: null
        );

        // 1️⃣ Buscar entidade existente
        _repositoryMock
            .Setup(r => r.GetById(psychologistId))
            .ReturnsAsync(_psychologist);

        // 2️⃣ Mapear request → entidade existente
        _mapperMock
            .Setup(m => m.Map(request, _psychologist))
            .Returns(_psychologist);

        // 3️⃣ Mapear entidade → response
        _mapperMock
            .Setup(m => m.Map<PsychologistResponse>(_psychologist))
            .Returns(expectedResponse);

        // Act
        var result = await _useCase.Execute(psychologistId, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(psychologistId, result.Id);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Crp, result.Crp);

        _repositoryMock.Verify(r => r.GetById(psychologistId), Times.Once);
        _mapperMock.Verify(m => m.Map(request, _psychologist), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.Is<Psychologist>(p => p.Id == psychologistId)), Times.Once);
    }
}