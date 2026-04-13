using System.ComponentModel.DataAnnotations;

namespace AthenaUnionLibrary.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MaxDateJson(string maxDate, string? errorMessage = null) : ValidationAttribute
    {
        private readonly DateTime _maxDate = DateTime.Parse(maxDate);
        private readonly string _errorMessage = errorMessage ?? $"The date must be less than or equal to {maxDate}.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            if (value is not DateTime date)
                return new ValidationResult("The field must be a valid date.", [validationContext.MemberName!]);

            if (date > _maxDate)
                return new ValidationResult(_errorMessage, [validationContext.MemberName!]);

            return ValidationResult.Success;
        }
    }
}
