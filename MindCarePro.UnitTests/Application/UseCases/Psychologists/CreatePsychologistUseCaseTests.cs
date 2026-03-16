using AutoMapper;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Domain.Entities.Psychologists;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Psychologists;

public class CreatePsychologistUseCaseTests
{
    private readonly Mock<IPsychologistRepository> _repositoryMock;
    private readonly Mock<IValidationService> _validationMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPasswordEncripter> _passwordEncripterMock;
    private readonly CreatePsychologistUseCase _useCase;
    private readonly Psychologist _psychologist;

    public CreatePsychologistUseCaseTests()
    {
        _psychologist = PsychologistFaker.Create();

        _repositoryMock = new Mock<IPsychologistRepository>();
        _repositoryMock
            .Setup(r => r.Add(It.IsAny<Psychologist>()))
            .Returns(Task.CompletedTask);

        _validationMock = new Mock<IValidationService>();
        _validationMock
            .Setup(v => v.ValidateAsync(It.IsAny<CreatePsychologistRequest>()))
            .Returns(Task.CompletedTask);

        _mapperMock = new Mock<IMapper>();
        _mapperMock
            .Setup(m => m.Map<Psychologist>(It.IsAny<CreatePsychologistRequest>()))
            .Returns(_psychologist);

        _passwordEncripterMock = new Mock<IPasswordEncripter>();
        _passwordEncripterMock
            .Setup(e => e.Encrypt(It.IsAny<string>()))
            .Returns("encrypted-password");

        _useCase = new CreatePsychologistUseCase(
            _repositoryMock.Object,
            _validationMock.Object,
            _passwordEncripterMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task ShouldCreatePsychologistCorrectly()
    {
        var request = new CreatePsychologistRequest(
            _psychologist.Name,
            _psychologist.Email,
            _psychologist.Cpf,
            _psychologist.Phone,
            _psychologist.Birth,
            _psychologist.Rg,
            _psychologist.Password,
            _psychologist.Crp
        );

        // Act
        var result = await _useCase.Execute(request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(_psychologist.Name, result.Value!.Name);
        Assert.Equal(_psychologist.Cpf, result.Value!.Cpf);
        Assert.Equal(_psychologist.Crp, result.Value!.Crp);

        _validationMock.Verify(v => v.ValidateAsync(request), Times.Once);
        _mapperMock.Verify(m => m.Map<Psychologist>(request), Times.Once);
        _passwordEncripterMock.Verify(e => e.Encrypt(request.Password), Times.Once);
        _repositoryMock.Verify(r => r.Add(It.Is<Psychologist>(p => p == _psychologist)), Times.Once);
    }
}
