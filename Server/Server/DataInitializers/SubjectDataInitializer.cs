using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class SubjectDataInitializer : ISubjectDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeData()
        {
            List<Subject> subjects = _unitOfWork.Subjects.GetAll();

            if (subjects.Count == 0)
            {
                _unitOfWork.Subjects.Add(new Subject()
                {
                    SubjectName = "Algebra"
                });

                _unitOfWork.Subjects.Add(new Subject()
                {
                    SubjectName = "Analiza"
                });

                _unitOfWork.Subjects.Add(new Subject()
                {
                    SubjectName = "Razvoj viseslojnog aplikacija u EES"
                });

                _unitOfWork.Subjects.Add(new Subject()
                {
                    SubjectName = "Cloud Computing"
                });

                _unitOfWork.Subjects.Add(new Subject()
                {
                    SubjectName = "Razvoj elektroenergetskog softvera"
                });

                _unitOfWork.Save();
            }

        }
    }
}
