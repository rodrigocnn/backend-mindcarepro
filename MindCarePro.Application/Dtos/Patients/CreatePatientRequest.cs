namespace MindCarePro.Application.Dtos.Patients;

public class CreatePatientRequest(
    string name,
    string email,
    string cpf,
    string phone,
    DateTime birthDate,
    string notes,
    string rg,
    string gender   
) 
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string Cpf { get; } = cpf;
    public string Phone { get; } = phone;
    public DateTime BirthDate { get; } = birthDate;
    public string Notes { get; } = notes;
    public string Rg { get; } = rg;
    public string Gender { get; } = gender;
}