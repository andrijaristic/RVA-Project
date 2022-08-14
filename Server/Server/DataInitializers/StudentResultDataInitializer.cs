using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class StudentResultDataInitializer : IStudentResultDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentResultDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeData()
        {
            List<StudentResult> studentResults = _unitOfWork.StudentResults.GetAll();

            if (studentResults.Count == 0)
            {
                _unitOfWork.StudentResults.Add(new StudentResult()
                {
                    ExamId = 1,
                    Exam = _unitOfWork.Exams.GetExam(1),
                    StudentId = 1,
                    Student = _unitOfWork.Students.GetStudent(1)
                });

                _unitOfWork.Save();
            }
        }

    }
}
