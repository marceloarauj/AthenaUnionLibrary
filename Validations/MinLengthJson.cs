using System.ComponentModel.DataAnnotations;

namespace AthenaUnionLibrary.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MinLengthJson(int minLength, string? errorMessage = null) : ValidationAttribute
    {
        private readonly int _minLength = minLength;
        private readonly string _errorMessage = errorMessage ?? $"The field must have a minimum of {minLength} characters.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            var stringValue = value.ToString()!;

            if (stringValue.Length < _minLength)
                return new ValidationResult(_errorMessage, [validationContext.MemberName!]);

            return ValidationResult.Success;
        }
    }
}
