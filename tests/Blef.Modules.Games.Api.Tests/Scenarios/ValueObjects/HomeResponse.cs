using JetBrains.Annotations;

namespace Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

[UsedImplicitly]
internal sealed record HomeResponse(string Module, DateTime RequestTime);