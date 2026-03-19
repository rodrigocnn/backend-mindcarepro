using AutoMapper;
using MindCarePro.Application.Dtos.Sessions;
using MindCarePro.Domain.Entities.Sessions;

namespace MindCarePro.Application.Mappers.Sessions;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<Session, SessionResponse>();
        CreateMap<UpdateSessionRequest, Session>();
    }
}
