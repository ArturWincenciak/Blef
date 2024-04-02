using System.ComponentModel.DataAnnotations;

namespace Blef.Modules.Games.Api.Controllers.Games.Validators;

internal sealed class NotWhitespaceAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new("Value cannot be null.");

        if (value is not string stringValue)
            return new("Value is not a string.");

        return string.IsNullOrWhiteSpace(stringValue)
            ? new("String value cannot be empty or whitespace.")
            : ValidationResult.Success;
    }
}