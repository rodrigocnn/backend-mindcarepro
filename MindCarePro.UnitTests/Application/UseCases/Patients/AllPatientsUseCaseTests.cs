using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Application.UseCases.Patients;
using MindCarePro.Domain.Entities.Patients;
using Moq;

namespace MindCarePro.UnitTests.Application.UseCases.Patients;

public class AllPatientsUseCaseTests
{
    [Fact]
    public async Task ShouldReturnAllPatients()
    {
     
        var patients = new List<Patient>
        {
            PatientFaker.Create(),
            PatientFaker.Create()
        };
        Guid userId = Guid.NewGuid();
       
        var repositoryMock = new Mock<IPatientRepository>();
        var currentUserMock = new Mock<ICurrentUser>();
        currentUserMock.SetupGet(c => c.UserId).Returns(userId);
        repositoryMock
            .Setup(repo => repo.GetAll(userId))
            .ReturnsAsync(patients);
        
        var useCase = new AllPatientsUseCase(repositoryMock.Object, currentUserMock.Object);
        var result = await useCase.Execute();
        
        Assert.NotNull(result);
        var enumerable = result as Patient[] ?? result.ToArray();
        Assert.Equal(2, enumerable.Count());
    
        repositoryMock.Verify(r => r.GetAll(userId), Times.Once);
    }
}
