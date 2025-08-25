

namespace SimpleECommerce.Core.Helpers
{
    using System.ComponentModel.DataAnnotations;
    public class ValidationHelper
    {
        internal static void Validate<T>(T obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);//false para validar solo las propiedades requeridas, true para validar todas las propiedades incluso la de las clases que se llaman

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);

            }
        }
    }

}
