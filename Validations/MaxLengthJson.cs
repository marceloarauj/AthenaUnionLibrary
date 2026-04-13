using System.ComponentModel.DataAnnotations;

namespace AthenaUnionLibrary.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MaxLengthJson(int maxLength, string? errorMessage = null) : ValidationAttribute
    {
        private readonly int _maxLength = maxLength;
        private readonly string _errorMessage = errorMessage ?? $"The field must have a maximum of {maxLength} characters.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            var stringValue = value.ToString()!;

            if (stringValue.Length > _maxLength)
                return new ValidationResult(_errorMessage, [validationContext.MemberName!]);

            return ValidationResult.Success;
        }
    }
}
