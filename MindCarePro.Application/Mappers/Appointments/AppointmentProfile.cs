using AutoMapper;
using MindCarePro.Application.Dtos.Appointments;
using MindCarePro.Domain.Entities.Appointments;


namespace MindCarePro.Application.Mappers.Appointments;

public class AppointmentProfile:Profile
{
    public AppointmentProfile()
    {
        
        CreateMap<Appointment, AppointmentResponse>();
        CreateMap<UpdateAppointmentRequest, Appointment>();

    }
}




