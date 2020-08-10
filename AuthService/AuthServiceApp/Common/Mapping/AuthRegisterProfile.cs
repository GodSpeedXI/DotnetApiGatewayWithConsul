using AuthServiceDomain.DTO;
using AuthServiceDomain.Entities;
using AutoMapper;

namespace AuthServiceApp.Common.Mapping
{
    public class AuthRegisterProfile : Profile
    {
        public AuthRegisterProfile()
        {
            CreateMap<AuthRegisterDto, UserModel>().ForMember(m => m.PasswordHash, opt => opt.Ignore());
            CreateMap<UserModel, AuthRegisterDto>();
            CreateMap<UserModel, AuthMemberDetailDto>();
        }
    }
}
