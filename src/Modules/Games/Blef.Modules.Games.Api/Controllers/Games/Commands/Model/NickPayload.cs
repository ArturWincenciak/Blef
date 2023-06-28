using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record NickPayload(
    [NotWhitespace] string Nick);
