﻿using JetBrains.Annotations;

namespace Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

internal enum WhichPlayer
{
    [UsedImplicitly]
    Knuth = 1,
    Graham = 2,
    Riemann = 4,
    Conway = 8
}
