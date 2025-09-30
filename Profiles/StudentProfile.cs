using AutoMapper;
using SzakdolgozatBackend.Dtos.Student;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentGetDto, Student>();
            CreateMap<StudentPatchDto, Student>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Student, StudentGetDto>();
        }
    }
}
