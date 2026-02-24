using MindCarePro.Domain.Entities.Users;

namespace MindCarePro.Domain.Entities.Assistant;

public class Assistant(
    string name, 
    DateTime birth, 
    string email, 
    string cpf, 
    string rg, 
    string password, 
    string phone) 
    : User(name, birth, email, cpf, rg, password, phone)
{
    protected Assistant() : this(null!, default, null!, null!, null!, null!, null!) 
    { 
    }
}