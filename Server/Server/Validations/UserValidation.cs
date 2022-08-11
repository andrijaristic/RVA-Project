using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class UserValidation : IValidation<User>
    {
        public ValidationResult Validate(User entity)
        {
            ValidationResult result = new ValidationResult() { isValid = true, Message = ""};

            if (string.IsNullOrEmpty(entity.Username.Trim()))
            {
                result.isValid = false;
                result.Message = "Username is invalid!";
                return result;
            }

            if (string.IsNullOrEmpty(entity.Password.Trim()))
            {
                result.isValid = false;
                result.Message = "Password is invalid!";
                return result;
            }

            if (string.IsNullOrEmpty(entity.Name.Trim()))
            {
                result.isValid = false;
                result.Message = "First name is invalid!";
                return result;
            }

            if (string.IsNullOrEmpty(entity.Lastname.Trim()))
            {
                result.isValid = false;
                result.Message = "Last name is invalid!";
                return result;
            }

            if (entity.UserType != Enums.EUserType.ADMIN && entity.UserType != Enums.EUserType.STUDENT)
            {
                result.isValid = false;
                result.Message = "User type is invalid!";
                return result;
            }

            return result;
        }
    }
}
