using AutoMapper;
using Server.Dto.ExamDto;
using Server.Dto.StudentDto;
using Server.Dto.StudentResultDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;
using System.Transactions;

namespace Server.Services
{
    public class StudentResultService : IStudentResultService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentResultService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;   
        }

        public async Task<DisplayExamDTO> AddStudentToExam(AddStudentResultDTO addStudentResultDTO)
        {
            Exam exam = await _unitOfWork.Exams.GetExamComplete(addStudentResultDTO.ExamId);
            if (exam == null)
            {
                throw new Exception($"Exam with ID [{addStudentResultDTO.ExamId}] doesn't exist.");
            }

            if (exam.ExamDate.ToLocalTime() <= DateTime.Now.ToLocalTime().AddDays(3))
            {
                throw new Exception($"Past exam application date.");
            }

            StudentResult studentResult = await _unitOfWork.StudentResults.GetStudentForExam(addStudentResultDTO.StudentId, addStudentResultDTO.ExamId);
            if (studentResult != null)
            {
                throw new Exception($"Student:ID[{addStudentResultDTO.StudentId}] is already registered to Exam:ID[{addStudentResultDTO.ExamId}]");
            }


            Student student =  await _unitOfWork.Students.GetStudentComplete(addStudentResultDTO.StudentId);
            student.Exams.Add(exam);

            studentResult = new StudentResult() { ExamId = exam.Id, StudentId = student.Id, Exam = exam, Student = student};
            await _unitOfWork.StudentResults.AddAsync(studentResult);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplayExamDTO>(exam);
        }

        public async Task<DisplayExamDTO> RemoveStudentFromExam(AddStudentResultDTO addStudentResultDTO)
        {            
            Exam exam = await _unitOfWork.Exams.GetExamAsync(addStudentResultDTO.ExamId);
            if (exam == null)
            {
                throw new Exception($"Exam with ID [{addStudentResultDTO.ExamId}] doesn't exist.");
            }

            StudentResult student = await _unitOfWork.StudentResults.GetStudentForExam(addStudentResultDTO.StudentId, addStudentResultDTO.ExamId);
            if (student == null)
            {
                throw new Exception($"Student with ID [{addStudentResultDTO.StudentId}] doesn't exist for {exam.ExamName}.");
            }

            foreach (Exam examEl in student.Student.Exams)
            {
                if (examEl.Id == exam.Id)
                {
                    student.Student.Exams.Remove(examEl);
                    break;
                }
            }

            if (student.isTouched)
            {
                throw new Exception($"Student with ID [{addStudentResultDTO.StudentId}] has already been graded for {exam.ExamName}.");
            }

            _unitOfWork.StudentResults.Remove(student);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplayExamDTO>(exam);
        }


        public async Task<List<DisplayStudentResultDTO>> GetAllStudentsForExam(int id)
        {
            List<StudentResult> students = await _unitOfWork.StudentResults.GetStudentsForExam(id);

            if (students == null || students.Count == 0)
            {
                throw new Exception($"There are no students registered for exam with ID [{id}].");
            }

            return _mapper.Map<List<DisplayStudentResultDTO>>(students);
        }

        public async Task<List<StudentExamsDTO>> GetExamsForStudent(int id)
        {
            List<StudentResult> exams = await _unitOfWork.StudentResults.GetExamsForStudent(id);
            if (exams == null)
            {
                throw new Exception($"Student with ID [{id}] has no registered exams.");
            }

            return _mapper.Map<List<StudentExamsDTO>>(exams);
        }

        public async Task<DisplayStudentResultDTO> GetResultsForStudent(AddStudentResultDTO dto)
        {
            StudentResult result = await _unitOfWork.StudentResults.GetStudentForExam(dto.StudentId, dto.ExamId);
            if (result == null)
            {
                throw new Exception($"Student with ID [{dto.StudentId}] doesn't exist for Exam: ID[{dto.ExamId}].");
            }

            return _mapper.Map<DisplayStudentResultDTO>(result);
         }

        public async Task<DisplayStudentResultDTO> GradeStudentExam(GradeStudentDTO dto)
        {
            StudentResult result = await _unitOfWork.StudentResults.GetStudentForExam(dto.StudentId, dto.ExamId);
            if (result == null)
            {
                throw new Exception($"Student with ID [{dto.StudentId}] doesn't exist for Exam: ID[{dto.ExamId}].");
            }

            Exam exam = await _unitOfWork.Exams.GetExamAsync(dto.ExamId);
            if (exam == null)
            {
                throw new Exception($"Exam ID[{dto.ExamId}] doesn't exist.");
            }

            if (exam.ExamDate.ToLocalTime() > DateTime.Now.ToLocalTime())
            {
                result.isTouched = true;
                throw new Exception($"Exam has not started. Cannot be graded.");
            }

            if (result.isTouched == true)
            {
                throw new Exception($"Student with ID [{dto.StudentId}] was already graded for Exam: ID[{dto.ExamId}].");
            }

            result.Result = dto.Result;
            result.isTouched = true;

            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplayStudentResultDTO>(result);
        }
    }
}
