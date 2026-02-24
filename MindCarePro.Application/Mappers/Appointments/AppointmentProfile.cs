using AutoMapper;
using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Domain.Entities.Appointments;


namespace MindCarePro.Application.Mappers.Appointments;

public class AppointmentProfile:Profile
{
    public AppointmentProfile()
    {
        
        CreateMap< Appointment, AppointmentResponse>();
  
        CreateMap<CreateAppointmentRequest, Appointment>()
            .ForCtorParam("title", opt => opt.MapFrom(src => src.Title))
            .ForCtorParam("start", opt => opt.MapFrom(src => src.Start))
            .ForCtorParam("end", opt => opt.MapFrom(src => src.End))
            .ForCtorParam("userId", opt => opt.MapFrom(src => src.UserId))
            .ForCtorParam("patientId", opt => opt.MapFrom(src => src.PatientId));

    }
}






