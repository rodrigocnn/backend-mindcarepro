using MindCarePro.Application.Interfaces.Psycholgists;
using MindCarePro.Application.Interfaces.Shared;
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
        var currentUserMock = new Mock<ICurrentUser>();
        currentUserMock.SetupGet(c => c.UserId).Returns(psychologists[0].Id);

        repositoryMock
            .Setup(repo => repo.GetAll(psychologists[0].Id))
            .ReturnsAsync(new[] { psychologists[0] });
        
        var useCase = new AllPsychologistsUseCase(repositoryMock.Object, currentUserMock.Object);
        var result = await useCase.Execute();
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        var enumerable = result.Value as Psychologist[] ?? result.Value!.ToArray();
        Assert.Single(enumerable);
    
        repositoryMock.Verify(r => r.GetAll(psychologists[0].Id), Times.Once);
    }
}
