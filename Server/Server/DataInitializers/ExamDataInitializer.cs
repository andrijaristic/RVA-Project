using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class ExamDataInitializer : IExamDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeData()
        {
            List<Exam> exams = _unitOfWork.Exams.GetAll();

            if (exams.Count == 0)
            {
                _unitOfWork.Exams.Add(new Exam()
                {
                    ExamName = "Ispit #1",
                    ExamDate = DateTime.Now,
                    SubjectId = 1,
                    Subject = _unitOfWork.Subjects.GetSubject(1),
                });

                _unitOfWork.Save();
            }
        }
    }
}
