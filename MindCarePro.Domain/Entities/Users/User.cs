using MindCarePro.Domain.Entities.BaseEntitie;


namespace MindCarePro.Domain.Entities.Users;

public class User(
    string name,
    DateTime birth,
    string email,
    string cpf,
    string rg,
    string password,
    string phone
) : BaseEntity
{
    // O erro acontece aqui se você não usar o ": this(...)"
    protected User() : this(null!, default, null!, null!, null!, null!, null!) 
    { 
    }

    public string Name { get; private set; } = name;
    public DateTime Birth { get; private set; } = birth;
    public string Email { get; set; } = email;
    public string Cpf { get; private set; } = cpf;
    public string Rg { get; private set; } = rg;
    public string Password { get;  set; } = password;
    public string Phone { get; private set; } = phone;
}