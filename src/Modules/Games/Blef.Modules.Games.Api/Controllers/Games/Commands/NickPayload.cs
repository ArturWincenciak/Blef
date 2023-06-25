using Blef.Shared.Infrastructure.Api.Validation;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

public sealed record NickPayload(
    [NotWhitespace] string Nick);
