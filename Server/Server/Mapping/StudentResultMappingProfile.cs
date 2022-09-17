using AutoMapper;
using Server.Dto.StudentResultDto;
using Server.Models;

namespace Server.Mapping
{
    public class StudentResultMappingProfile : Profile
    {
        public StudentResultMappingProfile()
        {
            CreateMap<StudentResult, DisplayStudentResultDTO>().ReverseMap();
            CreateMap<StudentResult, StudentExamsDTO>().ReverseMap();
        }
    }
}
