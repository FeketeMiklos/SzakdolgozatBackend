using AutoMapper;
using SzakdolgozatBackend.Dtos.LessonTime;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Profiles
{
    public class LessonTimeProfile : Profile
    {
        public LessonTimeProfile() 
        {
            CreateMap<LessonTimeCreateDto, LessonTime>();
            CreateMap<LessonTimeGetDto, LessonTime>();
            CreateMap<LessonTimePatchDto, LessonTime>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LessonTime, LessonTimeGetDto>();
        }
    }
}
