using System.Collections.Generic;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    public interface IDungeonAccessibilityProvider : IReactiveObject
    {
        bool Visible { get; }
        bool SequenceBreak { get; }
        int Accessible { get; }
        List<IBossAccessibilityProvider> BossAccessibilityProviders { get; }

        delegate IDungeonAccessibilityProvider Factory(IDungeon dungeon);
    }
}