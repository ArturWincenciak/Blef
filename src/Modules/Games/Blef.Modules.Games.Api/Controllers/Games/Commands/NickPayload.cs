using Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

public sealed record NickPayload(
    [NotWhitespace] string Nick);
