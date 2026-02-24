namespace MindCarePro.Application.Dtos.Psychologists;

public class UpdatePsychologistRequest(
    string name, 
    string email, 
    string cpf, 
    string phone, 
    DateTime birthDate, 
    string rg,
    string crp
) 
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string Cpf { get; } = cpf;
    public string Phone { get; } = phone;
    public DateTime BirthDate { get; } = birthDate;
    public string Rg { get; } = rg;
    public string Crp { get; } = crp;
}
