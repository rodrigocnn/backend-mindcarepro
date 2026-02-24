using AutoMapper;
using MindCarePro.Application.Dtos.Patients;
using MindCarePro.Domain.Entities.Patients;

namespace MindCarePro.Application.Mappers.Patients;

public class PatientProfile:Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientResponse>();
        
        
        // CreatePatientRequest → Patient (entrada)
        CreateMap<CreatePatientRequest, Patient>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("email", opt => opt.MapFrom(src => src.Email))
            .ForCtorParam("cpf", opt => opt.MapFrom(src => src.Cpf))
            .ForCtorParam("phone", opt => opt.MapFrom(src => src.Phone))
            .ForCtorParam("birthDate", opt => opt.MapFrom(src => src.BirthDate))
            .ForCtorParam("notes", opt => opt.MapFrom(src => src.Notes))
            .ForCtorParam("rg", opt => opt.MapFrom(src => src.Rg))
            .ForCtorParam("gender", opt => opt.MapFrom(src => src.Gender));
    }
}

