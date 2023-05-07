using AutoMapper;
using EducationGroupMicroService.Application.DTOs;
using EducationGroupMicroService.Domain;

namespace EducationGroupMicroService.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EducationGroup, EducationGroupDto>().ReverseMap();
            CreateMap<EducationGroup, CreateEducationGroupDto>().ReverseMap();
        }
    }
}
