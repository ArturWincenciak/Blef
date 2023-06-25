using System.ComponentModel.DataAnnotations;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

public class NotTheSameTwoPairsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Value cannot be null.");

        if (value is not TwoPairsBidPayload payload)
            return new ValidationResult($"Value is not a '{nameof(TwoPairsBidPayload)}'.");

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
