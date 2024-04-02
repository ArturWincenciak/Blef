using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Api.Controllers.Games.Commands;

namespace Blef.Modules.Games.Api.Controllers.Games.Validators;

internal sealed class FullHouseUniqueCardsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new("Value cannot be null.");

        if (value is not FullHouseBidPayload payload)
            return new($"Value is not a '{nameof(FullHouseBidPayload)}'");

        if (payload.ThreeOfAKind == payload.Pair)
            return new(
                errorMessage: "Three of a kind and pair cannot be the same card.",
                memberNames: new[]
                {
                    nameof(FullHouseBidPayload.ThreeOfAKind),
                    nameof(FullHouseBidPayload.Pair)
                });

        return ValidationResult.Success;
    }
}