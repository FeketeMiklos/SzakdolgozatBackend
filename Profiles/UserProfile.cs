using AutoMapper;
using SzakdolgozatBackend.Dtos.User;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserGetDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<UserPatchDto, User>()
                .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<ForgottenPasswordDto, User>();
            CreateMap<PasswordChangeDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.NewPassword));
            CreateMap<User, UserGetDto>();
        }
    }
}
