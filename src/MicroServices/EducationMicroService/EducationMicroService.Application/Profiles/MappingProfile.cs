using AutoMapper;
using EducationMicroService.Application.DTOs;
using EducationMicroService.Domain;

namespace EducationMicroService.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<Education, CreateEducationDto>().ReverseMap();
        }
    }
}
