using System.Collections.Generic;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains dungeon accessibility result data.
    /// </summary>
    public interface IDungeonResult
    {
        delegate IDungeonResult Factory(
            List<AccessibilityLevel> bossAccessibility, int accessible, bool visible);

        List<AccessibilityLevel> BossAccessibility { get; }
        int Accessible { get; }
        bool Visible { get; }
    }
}