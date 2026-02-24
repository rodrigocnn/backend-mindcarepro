using Bogus;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.UnitTests.Application.UseCases.Patients;

public static class PatientFaker
{
    public static Patient Create()
        => new Faker<Patient>()
            .CustomInstantiator(f => new Patient(
                f.Name.FullName(),
                f.Internet.Email(),
                f.Random.Replace("###.###.###-##"),
                f.Phone.PhoneNumber(),
                f.Date.Past(30, DateTime.Now.AddYears(-18)),
                "Patient of test",
                f.Random.Replace("##.###.###-#"),
                f.PickRandom("Masculino", "Feminino"),
                Guid.NewGuid() // atribuindo um UserId fake
            ));
}