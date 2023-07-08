using System.ComponentModel.DataAnnotations;

namespace Blef.Modules.Games.Api.Controllers.Games.Validators;

internal sealed class NotWhitespaceAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Value cannot be null.");

        if (value is not string stringValue)
            return new ValidationResult("Value is not a string.");

        return string.IsNullOrWhiteSpace(stringValue)
            ? new ValidationResult("String value cannot be empty or whitespace.")
            : ValidationResult.Success;
    }
}