using System.ComponentModel.DataAnnotations;

namespace Blef.Shared.Infrastructure.Api.Validation;

public class NotWhitespaceAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Value cannot be null.");

        if (value is not string stringValue)
            return new ValidationResult("Value is not a string.");

        if (string.IsNullOrWhiteSpace(stringValue))
            return new ValidationResult("String value cannot be empty or whitespace.");

        return ValidationResult.Success;
    }
}