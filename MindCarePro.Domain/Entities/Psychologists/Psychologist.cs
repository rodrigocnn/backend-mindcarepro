using MindCarePro.Domain.Entities.Users;

namespace MindCarePro.Domain.Entities.Psychologists;

public class Psychologist(
    string name, 
    DateTime birth, 
    string email, 
    string cpf, 
    string rg, 
    string password, 
    string phone, 
    string crp) : User(name, birth, email, cpf, rg, password, phone)
{
    // Chama o Primary Constructor da própria classe Psychologist
    protected Psychologist() : this(null!, default, null!, null!, null!, null!, null!, null!) 
    { 
    }

    public string Crp { get; private set; } = crp;

 
}