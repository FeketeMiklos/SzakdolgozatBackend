using AutoMapper;
using SzakdolgozatBackend.Dtos.Lesson;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Profiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<LessonCreateDto, Lesson>();
            CreateMap<LessonGetDto, Lesson>();
            CreateMap<LessonPatchDto, Lesson>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Lesson, LessonGetDto>();
        }
    }
}
