using System.ComponentModel.DataAnnotations;

namespace AthenaUnionLibrary.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class RequiredJson(string? errorMessage = null) : ValidationAttribute
    {
        private readonly string _errorMessage = errorMessage ?? "The field is required.";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
                return new ValidationResult(_errorMessage, [validationContext.MemberName!]);

            return ValidationResult.Success;
        }
    }
}
