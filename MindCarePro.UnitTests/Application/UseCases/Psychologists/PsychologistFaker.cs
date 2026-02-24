using Bogus;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.UnitTests.Application.UseCases.Psychologists
{
    public static class PsychologistFaker
    {
        public static Psychologist Create()
            => new Faker<Psychologist>()
                .CustomInstantiator(f => new Psychologist(
                    f.Name.FullName(),                                      
                    f.Date.Past(30, DateTime.Now.AddYears(-18)),             
                    f.Internet.Email(),                                    
                    f.Random.Replace("###.###.###-##"),                    
                    f.Random.Replace("##.###.###-#"),                     
                    f.Internet.Password(8, false, "", "!1Aa"),            
                    f.Phone.PhoneNumber("(##) #####-####"),                
                    $"CRP-{f.Random.Number(10000, 99999)}"             
                ));
    }
}