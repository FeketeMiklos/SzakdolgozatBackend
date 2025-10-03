using AutoMapper;
using SzakdolgozatBackend.Dtos.Signature;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Profiles
{
    public class SignatureProfile : Profile
    {
        public SignatureProfile()
        {
            CreateMap<Signature, SignatureGetDto>();
            CreateMap<SignatureCreateDto, Signature>();
            CreateMap<SignaturePatchDto, Signature>()
                .ForMember(x => x.StudentNeptunCode, opt => opt.Ignore())
                .ForMember(x => x.LessonId, opt => opt.Ignore())
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<SignatureGetDto, Signature>();
        }
    }
}
