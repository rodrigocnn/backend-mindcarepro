using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.UseCases.Psychologists;
using MindCarePro.Domain.Entities.Psychologists;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Psychologists;

public class AllPsychologistsUseCaseTests
{
    [Fact]
    public async Task ShouldReturnAllPsychologists()
    {
        var psychologists = new List<Psychologist>
        {
            PsychologistFaker.Create(),
            PsychologistFaker.Create()
        };
        
        var repositoryMock = new Mock<IPsychologistRepository>();
        repositoryMock
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(psychologists);
        
        var useCase = new AllPsychologistsUseCase(repositoryMock.Object);
        var result = await useCase.Execute();
        
        Assert.NotNull(result);
        var enumerable = result as Psychologist[] ?? result.ToArray();
        Assert.Equal(2, enumerable.Count());
    
        repositoryMock.Verify(r => r.GetAll(), Times.Once);
    }
}