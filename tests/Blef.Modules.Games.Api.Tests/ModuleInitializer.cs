using System.Runtime.CompilerServices;

namespace Blef.Modules.Games.Api.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() =>
        VerifyDiffPlex.Initialize();

    [ModuleInitializer]
    public static void OtherInitialize()
    {
        VerifierSettings.InitializePlugins();
        VerifierSettings.ScrubLinesContaining("DiffEngineTray");
        VerifierSettings.IgnoreStackTrace();
    }
}