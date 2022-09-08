using AutoMapper;
using Server.Dto.ExamDto;
using Server.Models;

namespace Server.Mapping
{
    public class ExamMappingProfile : Profile
    {
        public ExamMappingProfile()
        {
            CreateMap<Exam, DetailedExamDTO>().ReverseMap();
            CreateMap<Exam, DisplayExamDTO>().ReverseMap();
            CreateMap<Exam, NewExamDTO>().ReverseMap();
        }
    }
}
