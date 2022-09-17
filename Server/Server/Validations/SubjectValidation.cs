using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations
{
    public class SubjectValidation : IValidation<Subject>
    {
        public ValidationResult Validate(Subject entity)
        {
            ValidationResult result = new ValidationResult() { isValid = true, Message = "" };

            if (string.IsNullOrEmpty(entity.SubjectName) || string.IsNullOrWhiteSpace(entity.SubjectName))
            {
                result.isValid = false;
                result.Message = "Subject name is invalid!";
                return result;
            }

            return result;
        }
    }
}
