using MindCarePro.Application.Interfaces;
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
        
        var repositoryMock = new Mock<IPatientRepository>();
        repositoryMock
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(patients);
        
        var useCase = new AllPatientsUseCase(repositoryMock.Object);
        var result = await useCase.Execute();
        
        Assert.NotNull(result);
        var enumerable = result as Patient[] ?? result.ToArray();
        Assert.Equal(2, enumerable.Count());
    
        repositoryMock.Verify(r => r.GetAll(), Times.Once);
    }
}