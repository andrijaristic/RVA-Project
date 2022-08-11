using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.DataInitializerInterfaces;
using Server.Models;

namespace Server.DataInitializers
{
    public class UserDataInitializer : IUserDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeData()
        {
            List<User> users = _unitOfWork.Users.GetAll();
            
            if (users.Count == 0)
            {
                _unitOfWork.Users.Add(new User() 
                { 
                    Name = "Admin", 
                    Lastname = "Admin", 
                    Username = "admin", 
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                    UserType = Enums.EUserType.ADMIN
                });

                _unitOfWork.Save();
            }
        }
    }
}
