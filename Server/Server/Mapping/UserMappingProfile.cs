using AutoMapper;
using Server.Dto.UserDto;
using Server.Models;

namespace Server.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, AuthDTO>().ReverseMap();
            CreateMap<User, UpdateDTO>().ReverseMap();
        }
    }
}
