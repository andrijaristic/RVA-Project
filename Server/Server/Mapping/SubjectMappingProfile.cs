using AutoMapper;
using Server.Dto.SubjectDto;
using Server.Models;

namespace Server.Mapping
{
    public class SubjectMappingProfile : Profile
    {
        public SubjectMappingProfile()
        {
            CreateMap<Subject, DisplaySubjectDTO>().ReverseMap();
        }
    }
}
