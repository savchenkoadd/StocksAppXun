using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
	internal static class ValidationHelper
	{
		internal static List<ValidationResult> GetValidationErrors(object? obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj));
			}

			ValidationContext validationContext = new ValidationContext(obj);

			List<ValidationResult> errors = new List<ValidationResult>();

			Validator.TryValidateObject(obj, validationContext, errors);

			return errors;
		}
	}
}
