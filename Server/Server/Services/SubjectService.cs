using AutoMapper;
using Server.Dto.SubjectDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplaySubjectDTO>(subject);
        }

        public async Task<string> DeleteSubject(int id)
        {
            Subject subject = await _unitOfWork.Subjects.GetSubjectAsync(id);
            if (subject == null)
            {
                throw new Exception($"Subject with ID [{id}] doesn't exist.");
            }

            _unitOfWork.Subjects.Remove(subject);
            await _unitOfWork.SaveAsync();

            return $"Subject [{subject.Id}|{subject.SubjectName}] successfully removed!";
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
