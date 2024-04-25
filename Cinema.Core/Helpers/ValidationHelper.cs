using System.ComponentModel.DataAnnotations;

namespace Cinema.Core.Helpers;

public class ValidationHelper
{
    /// <summary>
    /// Performs model validation on the specified object using data annotations(validation attributes).
    /// </summary>
    /// <param name="obj">The object to be validated.</param>
    /// <exception cref="ArgumentException">Thrown if the object fails validation based on data annotations.</exception>
    internal static void ModelValidation(object obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}