
using MindCarePro.Application.Dtos.Sessions;
using MindCarePro.Application.Interfaces;
using MindCarePro.Application.Interfaces.Sessions;
using MindCarePro.Application.Interfaces.Shared;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Shared;

namespace MindCarePro.Application.UseCases.Sessions;

public class  AttendedPatientUseCase(
    ISessionRepository sessionRepository,
    ICurrentUser currentUser,
    IPatientRepository patientRepository)
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<Result<AttendPatientResponse>> Execute(Guid id)
    {
        if (_currentUser.UserId is null)
        {
            return Result<AttendPatientResponse>.Failure(ResultErrorType.Unauthorized, "Acesso não autorizado");
        }

        var userId = _currentUser.UserId.Value;
        var patient = await _patientRepository.GetById(id, userId);
        
        if (patient is null)
        {
            return Result<AttendPatientResponse>.Failure(ResultErrorType.NotFound, "Paciente não encontrado");
        }

        var sessions = await _sessionRepository.GetAllByPatient(userId, patient.Id);
        var qtdServices = sessions.Count();
        var firstSessionDate = sessions
            .OrderBy(s => s.SessionDate)
            .Select(s => s.SessionDate)
            .FirstOrDefault();

        var initialDate = qtdServices == 0 || firstSessionDate == default
            ? "-"
            : firstSessionDate.ToString("O");

        var response = new AttendPatientResponse(
            name: patient.Name,
            age: GetAge(patient),
            qtdServices: qtdServices,
            initialDate: initialDate
        );

        return Result<AttendPatientResponse>.Success(response);
    }
    
    private int GetAge(Patient patient)
    {
        var yearBirth = patient.BirthDate.Year;
        var currentYear = DateTime.Now.Year;
        var age = currentYear - yearBirth;
        return age;
    }
}
