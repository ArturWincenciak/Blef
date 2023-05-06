using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Services;

internal interface IDeckFactory
{
    Deck Create();
}