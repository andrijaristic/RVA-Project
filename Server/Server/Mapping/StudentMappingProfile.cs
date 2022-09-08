using AutoMapper;
using Server.Dto.StudentDto;
using Server.Dto.UserDto;
using Server.Models;

namespace Server.Mapping
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, RegisterDTO>().ReverseMap();
            CreateMap<Student, DisplayStudentDTO>().ReverseMap();
        }
    }
}
