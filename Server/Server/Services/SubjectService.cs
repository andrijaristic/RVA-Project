using AutoMapper;
using Server.Dto;
using Server.Dto.SubjectDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidation<Subject> _subjectValidation;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper, IValidation<Subject> subjectValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _subjectValidation = subjectValidation;
        }

        public async Task<List<DetailedSubjectDTO>> GetAllSubjectsComplete()
        {
            List<Subject> subjects = await _unitOfWork.Subjects.GetSubjectsComplete();
            if (subjects == null)
            {
                throw new Exception($"There are no subjects.");
            }

            return _mapper.Map<List<DetailedSubjectDTO>>(subjects);
        }

        public async Task<DisplaySubjectDTO> CreateSubject(NewSubjectDTO newSubjectDTO)
        {
            Subject subject = _mapper.Map<Subject>(newSubjectDTO);

            ValidationResult result = _subjectValidation.Validate(subject);
            if (!result.isValid)
            {
                throw new Exception(result.Message);
            }

            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplaySubjectDTO>(subject);
        }

        public async Task<SuccessDTO> DeleteSubject(int id)
        {
            Subject subject = await _unitOfWork.Subjects.GetSubjectAsync(id);
            if (subject == null)
            {
                throw new Exception($"Subject with ID [{id}] doesn't exist.");
            }

            List<Exam> exams = await _unitOfWork.Exams.GetExamsForSubject(id);
            if (exams != null)
            {
                throw new Exception($"Subject with ID [{id}] has registered exams. Cannot be deleted.");
            }

            _unitOfWork.Subjects.Remove(subject);
            await _unitOfWork.SaveAsync();

            SuccessDTO response = new SuccessDTO() { Title = "Successful subject removal", Message = $"Subject [{subject.Id}|{subject.SubjectName}] successfully removed!" };
            return response;
        }

        public async Task<SuccessDTO> UpdateSubject(SubjectUpdateDTO dto)
        {
            Subject subject = await _unitOfWork.Subjects.GetSubjectAsync(dto.Id);
            if (subject == null)
            {
                throw new Exception($"Subject with ID [{dto.Id}] doesn't exist.");
            }

            subject.SubjectName = dto.SubjectName;
            await _unitOfWork.SaveAsync();

            SuccessDTO response = new SuccessDTO() { Title = "Successful subject update", Message = $"Subject [{subject.Id}|{subject.SubjectName}] successfully updated!" };
            return response;
        }

        public async Task<DetailedSubjectDTO> GetSubjectComplete(int id)
        {
            Subject subject = await _unitOfWork.Subjects.GetSubjectComplete(id);
            if (subject == null)
            {
                throw new Exception($"Subject with ID [{id}] doesn't exist.");
            }

            return _mapper.Map<DetailedSubjectDTO>(subject);
        }
    }
}
