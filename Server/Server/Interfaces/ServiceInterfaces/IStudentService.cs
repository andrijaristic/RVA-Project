using Server.Dto.StudentDto;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface IStudentService
    {
        Task<DisplayStudentDTO> GetStudentId(string username);
        Task<List<DetailedStudentDTO>> GetStudentsDetailed();
    }
}
