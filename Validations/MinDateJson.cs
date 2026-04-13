using System.ComponentModel.DataAnnotations;

namespace AthenaUnionLibrary.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MinDateJson(string minDate, string? errorMessage = null) : ValidationAttribute
    {
        private readonly DateTime _minDate = DateTime.Parse(minDate);
        private readonly string _errorMessage = errorMessage ?? $"The date must be greater than or equal to {minDate}.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            if (value is not DateTime date)
                return new ValidationResult("The field must be a valid date.", [validationContext.MemberName!]);

            if (date < _minDate)
                return new ValidationResult(_errorMessage, [validationContext.MemberName!]);

            return ValidationResult.Success;
        }
    }
}
