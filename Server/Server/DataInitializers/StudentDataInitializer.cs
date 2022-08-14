using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class StudentDataInitializer : IStudentDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeData()
        {
            List<Student> students = _unitOfWork.Students.GetAll();

            if (students.Count != 0)
            {
                return;
            }

            List<User> users = _unitOfWork.Users.GetAllStudentUsers();

            if (users.Count != 0)
            {
                foreach (var el in users)
                {
                    _unitOfWork.Students.Add(new Student()
                    {
                        Name = el.Name,
                        LastName = el.Lastname
                    });
                }

                _unitOfWork.Save();
                return;
            }

            // Kreiraj novog studenta.
            _unitOfWork.Students.Add(new Student()
            {
                Name = "Pera",
                LastName = "Peric"
            });

            _unitOfWork.Save();
        }
    }
}
