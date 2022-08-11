namespace Server.Interfaces.ValidationInterfaces
{
    public interface IValidation<T> where T : class
    {
        ValidationResult Validate(T entity);
    }
}
