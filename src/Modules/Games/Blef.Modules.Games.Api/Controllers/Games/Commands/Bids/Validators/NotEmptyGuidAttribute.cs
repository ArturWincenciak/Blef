using System.ComponentModel.DataAnnotations;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

public class NotEmptyGuidAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Guid value cannot be null.");

        if (value is not Guid guidValue)
            return new ValidationResult("Value is not a Guid.");

        if (guidValue == Guid.Empty)
            return new ValidationResult("Guid value cannot be empty.");

        return ValidationResult.Success;
    }
}
