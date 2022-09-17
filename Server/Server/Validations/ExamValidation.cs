using Server.Interfaces.ValidationInterfaces;
using Server.Models;

namespace Server.Validations 
{
    public class ExamValidation : IValidation<Exam>
    {
        public ValidationResult Validate(Exam entity)
        {
            ValidationResult result = new ValidationResult() { isValid = true, Message = "" };

            if (string.IsNullOrEmpty(entity.ExamName) || string.IsNullOrWhiteSpace(entity.ExamName))
            {
                result.isValid = false;
                result.Message = "Exam name is invalid!";
                return result;
            }

            if (entity.ExamDate <= DateTime.Now.ToLocalTime()) 
            {
                result.isValid = false;
                result.Message = "Exam date is in the past.";
                return result;
            }

            return result;
        }
    }
}
