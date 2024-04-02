using System.ComponentModel.DataAnnotations;

namespace Blef.Modules.Games.Api.Controllers.Games.Validators;

internal sealed class NotEmptyGuidAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new("Guid value cannot be null.");

        if (value is not Guid guidValue)
            return new("Value is not a Guid.");

        return guidValue == Guid.Empty
            ? new("Guid value cannot be empty.")
            : ValidationResult.Success;
    }
}