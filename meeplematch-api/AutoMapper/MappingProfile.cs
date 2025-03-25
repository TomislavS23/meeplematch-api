using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;

namespace meeplematch_api.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Role, RoleDTO>();
        CreateMap<RoleDTO, Role>();

        CreateMap<User, UserDTO>().ForMember(x => x.IdUser, opt => opt.Ignore());
        CreateMap<UserDTO, User>().ForMember(x => x.IdUser, opt => opt.Ignore());
        CreateMap<RegisterDTO, User>();
        CreateMap<User, RegisterDTO>();
    }
}