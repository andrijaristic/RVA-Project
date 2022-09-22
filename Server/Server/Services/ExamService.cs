using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Dto;
using Server.Dto.ExamDto;
using Server.Dto.StudentResultDto;
using Server.Dto.SubjectDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Services
{
    public class ExamService : IExamService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidation<Exam> _examValidation;
        
        public ExamService(IUnitOfWork unitOfWork, IMapper mapper, IValidation<Exam> examValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _examValidation = examValidation;
        }

        public async Task<DisplayExamDTO> CreateExam(NewExamDTO newExamDTO)
        {
            Exam exam = _mapper.Map<Exam>(newExamDTO);
            ValidationResult result = _examValidation.Validate(exam);
            if (!result.isValid)
            {
                throw new Exception(result.Message);
            }

            Subject subject = await _unitOfWork.Subjects.GetSubjectAsync(newExamDTO.SubjectId);
            if (subject == null)
            {
                throw new Exception("Subject doesn't exist.");
            }
            exam.Subject = subject;

            await _unitOfWork.Exams.AddAsync(exam);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplayExamDTO>(exam);
        }

        public async Task<SuccessDTO> DeleteExam(int id)
        {
            Exam exam = await _unitOfWork.Exams.GetExamAsync(id);
            if (exam == null)
            {
                throw new Exception($"Exam with ID[{id}] doesn't exist.");
            }

            if (exam.ExamDate.ToLocalTime() < DateTime.Now.ToLocalTime())
            {
                throw new Exception($"Exam is past date. Cannot be deleted.");
            }

            List<StudentResult> registeredStudents = await _unitOfWork.StudentResults.GetStudentsForExam(id);
            if (registeredStudents != null)
            {
                throw new Exception($"Exam with ID[{id}] has registered students. Cannot be deleted.");
            }

            _unitOfWork.Exams.Remove(exam);
            await _unitOfWork.SaveAsync();

            SuccessDTO response = new SuccessDTO() { Title = "Successful exam deletion", Message = $"Exam with ID [{id}] has been successfully deleted!"};
            return response;
        }

        public async Task<SuccessDTO> UpdateExam(UpdateExamDTO dto)
        {
            Exam exam = await _unitOfWork.Exams.GetExamAsync(dto.Id);
            if (exam == null)
            {
                throw new Exception($"Exam with ID[{dto.Id}] doesn't exist.");
            }

            Exam updatedExam = _mapper.Map<Exam>(dto);
            updatedExam.ExamDate = exam.ExamDate;

            ValidationResult validation = _examValidation.Validate(updatedExam);
            if (!validation.isValid)
            {
                throw new Exception(validation.Message);
            }

            exam.ExamName = dto.ExamName;
            await _unitOfWork.SaveAsync();

            SuccessDTO response = new SuccessDTO() { Title = "Successful exam update", Message = $"Exam with ID [{dto.Id}] has been successfully updated!" };
            return response;
        }

        public async Task<List<DetailedExamDTO>> GetAllExams()
        {
            List<Exam> exams = await _unitOfWork.Exams.GetAllExamsComplete();
            if (exams == null)
            {
                throw new Exception("There are no exams.");
            }

            return _mapper.Map<List<DetailedExamDTO>>(exams);
        }

        public async Task<DetailedExamDTO> GetById(int id)
        {
            Exam exam = await _unitOfWork.Exams.GetExamComplete(id);
            if (exam == null)
            {
                throw new Exception($"Exam with ID[{id}] doesn't exist.");
            }

            DetailedExamDTO dto = _mapper.Map<DetailedExamDTO>(exam);

            return dto;
        }


    }
}
