using Blef.Modules.Games.Api.Controllers.Games.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record NickPayload(
    [NotWhitespace] string Nick);