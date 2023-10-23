using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.CustomAttributes
{
	public class CustomDateTimeRangeAttribute : ValidationAttribute
	{
		private readonly DateTime _minDate;
		private readonly DateTime _maxDate;

		public CustomDateTimeRangeAttribute(string minDate, string maxDate)
		{
			_minDate = DateTime.Parse(minDate);
			_maxDate = DateTime.Parse(maxDate);
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value is DateTime dateTimeValue)
			{
				if (dateTimeValue >= _minDate && dateTimeValue <= _maxDate)
				{
					return ValidationResult.Success;
				}
				else
				{
					return new ValidationResult($"The field {validationContext.DisplayName} must be between {_minDate.ToShortDateString()} and {_maxDate.ToShortDateString()}.");
				}
			}

			return new ValidationResult("Invalid date format.");
		}
	}
}
