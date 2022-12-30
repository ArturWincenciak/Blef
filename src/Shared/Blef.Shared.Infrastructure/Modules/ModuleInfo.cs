using JetBrains.Annotations;

namespace Blef.Shared.Infrastructure.Modules;

[PublicAPI]
internal record ModuleInfo(string Name, string Path);