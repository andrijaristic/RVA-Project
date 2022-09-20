using AutoMapper;
using Server.Dto.StudentDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DisplayStudentDTO> GetStudentId(string username)
        {
            Student student = await _unitOfWork.Students.GetStudentForUser(username);
            if (student == null)
            {
                throw new Exception("Student does not exist.");
            }

            return _mapper.Map<DisplayStudentDTO>(student);
        }

        public async Task<List<DetailedStudentDTO>> GetStudentsDetailed()
        {
            List<Student> students = await _unitOfWork.Students.GetStudentsDetailed();
            if (students == null)
            {
                throw new Exception("There are no registered students.");
            }

            foreach (var el in students)
            {
                List<StudentResult> exams = await _unitOfWork.StudentResults.GetExamsForStudent(el.Id);
                foreach (var _el in exams)
                {
                    el.Exams.Add(_el.Exam);
                }
            }

            return _mapper.Map<List<DetailedStudentDTO>>(students);
        }
    }
}
