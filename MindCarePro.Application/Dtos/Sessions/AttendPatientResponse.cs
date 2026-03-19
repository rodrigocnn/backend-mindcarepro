namespace MindCarePro.Application.Dtos.Sessions;

public class AttendPatientResponse(
    string name,
    int age,
    int qtdServices,
    string initialDate
)
{
    public string Name { get; } = name;
    public int Age { get; } = age;
    public int QtdServices { get; } = qtdServices;
    public string InitialDate { get; } = initialDate;
}
