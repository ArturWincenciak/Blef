using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

public class TwoPairsUniqueCardsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Value cannot be null.");

        if (value is not TwoPairsBidPayload payload)
            throw new ArgumentException($"Value is not a '{nameof(TwoPairsBidPayload)}'", nameof(value));

        if(payload.FirstFaceCard == payload.SecondFaceCard)
            return new ValidationResult(
                errorMessage: "Face cards must be different than each other but are the same.",
                memberNames: new[]
                {
                    nameof(TwoPairsBidPayload.FirstFaceCard),
                    nameof(TwoPairsBidPayload.SecondFaceCard)
                });

        return ValidationResult.Success;
    }
}
