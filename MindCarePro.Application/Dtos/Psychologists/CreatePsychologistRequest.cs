namespace MindCarePro.Application.Dtos.Psychologists;

public class CreatePsychologistRequest(
    string name, 
    string email, 
    string cpf, 
    string phone, 
    DateTime birthDate, 
    string rg, 
    string password, 
    string crp
) 
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string Cpf { get; } = cpf;
    public string Phone { get; } = phone;
    public DateTime BirthDate { get; } = birthDate;
    public string Rg { get; } = rg;
    public string Password { get; } = password;
    public string Crp { get; } = crp;
 
}

