using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record NickPayload(
    [NotWhitespace] string Nick);