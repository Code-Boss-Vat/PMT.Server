
using System.ComponentModel.DataAnnotations;

namespace PMT.Core.Helpers;

internal class ValidationHelper
{
    public static bool TryValidate<T>(T instance, out List<ValidationResult> results) where T : class
    {
        var context = new ValidationContext(instance);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(instance, context, results, validateAllProperties: true);
    }
}