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

        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
        CreateMap<RegisterDTO, User>();
        CreateMap<User, RegisterDTO>();

        CreateMap<Event, EventDTO>();
        CreateMap<EventDTO, Event>();

        CreateMap<User, PublicUserDTO>().ReverseMap();
        CreateMap<User, CreateUserDTO>().ReverseMap();
        CreateMap<UserDTO, PublicUserDTO>();

        CreateMap<EventParticipant, EventParticipantDTO>();
        CreateMap<EventParticipantDTO, EventParticipant>();

        CreateMap<EventCommentDTO, EventComment>();
        CreateMap<EventComment, EventCommentDTO>();

        CreateMap<Report, ReportDTO>();
        CreateMap<ReportDTO, Report>();
        
        CreateMap<ReportStatus, ReportStatusDTO>();
        CreateMap<ReportStatusDTO, ReportStatus>();
    }
}