using AutoMapper;
using MindCarePro.Application.Dtos.Psychologists;
using MindCarePro.Domain.Entities.Psychologists;

namespace MindCarePro.Application.Mappers.Psycholists;

public class PsychologistProfile : Profile
{
    public PsychologistProfile()
    {
        // Request -> Entity (create)
        CreateMap<CreatePsychologistRequest, Psychologist>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("birth", opt => opt.MapFrom(src => src.BirthDate))
            .ForCtorParam("email", opt => opt.MapFrom(src => src.Email))
            .ForCtorParam("cpf", opt => opt.MapFrom(src => src.Cpf))
            .ForCtorParam("rg", opt => opt.MapFrom(src => src.Rg))
            .ForCtorParam("password", opt => opt.MapFrom(_ => string.Empty)) 
            .ForCtorParam("phone", opt => opt.MapFrom(src => src.Phone))
            .ForCtorParam("crp", opt => opt.MapFrom(src => src.Crp));
      
        
        CreateMap<UpdatePsychologistRequest, Psychologist>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("birth", opt => opt.MapFrom(src => src.BirthDate))
            .ForCtorParam("email", opt => opt.MapFrom(src => src.Email))
            .ForCtorParam("cpf", opt => opt.MapFrom(src => src.Cpf))
            .ForCtorParam("rg", opt => opt.MapFrom(src => src.Rg))
            .ForCtorParam("phone", opt => opt.MapFrom(src => src.Phone))
            .ForCtorParam("crp", opt => opt.MapFrom(src => src.Crp));
        

        // Entity -> Response
        CreateMap<Psychologist, PsychologistResponse>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("birth", opt => opt.MapFrom(src => src.Birth))
            .ForCtorParam("email", opt => opt.MapFrom(src => src.Email))
            .ForCtorParam("cpf", opt => opt.MapFrom(src => src.Cpf))
            .ForCtorParam("rg", opt => opt.MapFrom(src => src.Rg))
            .ForCtorParam("phone", opt => opt.MapFrom(src => src.Phone))
            .ForCtorParam("crp", opt => opt.MapFrom(src => src.Crp))
            .ForCtorParam("createdAt", opt => opt.MapFrom(src => src.CreatedAt))
            .ForCtorParam("updatedAt", opt => opt.MapFrom(src => src.UpdatedAt))
            .ForCtorParam("deletedAt", opt => opt.MapFrom(src => src.DeletedAt));
    }
}