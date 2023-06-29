using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

internal sealed class FullHouseUniqueCardsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Value cannot be null.");

        if (value is not FullHouseBidPayload payload)
            throw new ArgumentException($"Value is not a '{nameof(FullHouseBidPayload)}'", nameof(value));

        if (payload.ThreeOfAKind == payload.Pair)
            return new ValidationResult(
                errorMessage: "Three of a kind and pair cannot be the same card.",
                memberNames: new[]
                {
                    nameof(FullHouseBidPayload.ThreeOfAKind),
                    nameof(FullHouseBidPayload.Pair)
                });

        return ValidationResult.Success;
    }
}
