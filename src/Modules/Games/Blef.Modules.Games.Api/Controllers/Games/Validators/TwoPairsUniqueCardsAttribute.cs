using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Api.Controllers.Games.Commands;

namespace Blef.Modules.Games.Api.Controllers.Games.Validators;

internal sealed class TwoPairsUniqueCardsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new("Value cannot be null.");

        if (value is not TwoPairsBidPayload payload)
            return new($"Value is not a '{nameof(TwoPairsBidPayload)}'");

        if (payload.FirstFaceCard == payload.SecondFaceCard)
            return new(
                errorMessage: "Face cards must be different than each other but are the same.",
                memberNames: new[]
                {
                    nameof(TwoPairsBidPayload.FirstFaceCard),
                    nameof(TwoPairsBidPayload.SecondFaceCard)
                });

        return ValidationResult.Success;
    }
}