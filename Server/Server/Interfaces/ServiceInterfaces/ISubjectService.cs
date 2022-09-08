using Server.Dto.SubjectDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface ISubjectService
    {
        Task<List<DetailedSubjectDTO>> GetAllSubjectsComplete();
        Task<DetailedSubjectDTO> GetSubjectComplete(int id);
        Task<DisplaySubjectDTO> CreateSubject(NewSubjectDTO newSubjectDTO);
        Task<string> DeleteSubject(int id);
    }
}
