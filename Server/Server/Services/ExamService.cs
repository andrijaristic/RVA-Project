﻿using AutoMapper;
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

        public async Task DeleteExam(int id)
        {
            Exam exam = await _unitOfWork.Exams.GetExamAsync(id);
            if (exam == null)
            {
                throw new Exception($"Exam with ID[{id}] doesn't exist.");
            }

            _unitOfWork.Exams.Remove(exam);
            await _unitOfWork.SaveAsync();
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
                throw new Exception("Exception");
            }

            DetailedExamDTO dto = _mapper.Map<DetailedExamDTO>(exam);

            return dto;
        }
    }
}
