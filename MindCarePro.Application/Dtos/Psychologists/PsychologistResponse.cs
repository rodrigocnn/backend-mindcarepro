namespace MindCarePro.Application.Dtos.Psychologists;

public class PsychologistResponse(
    Guid id,
    string name,
    DateTime birth,
    string email,
    string cpf,
    string rg,
    string phone,
    string crp,
    DateTime createdAt,
    DateTime updatedAt,
    DateTime? deletedAt
)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public DateTime Birth { get; } = birth;
    public string Email { get; } = email;
    public string Cpf { get; } = cpf;
    public string Rg { get; } = rg;
    public string Phone { get; } = phone;
    public string Crp { get; } = crp;
    public DateTime CreatedAt { get; } = createdAt;
    public DateTime UpdatedAt { get; } = updatedAt;
    public DateTime? DeletedAt { get; } = deletedAt;
}