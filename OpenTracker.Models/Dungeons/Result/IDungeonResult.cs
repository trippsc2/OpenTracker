using System.Collections.Generic;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Dungeons.Result
{
    /// <summary>
    ///     This interface contains dungeon accessibility result data.
    /// </summary>
    public interface IDungeonResult
    {
        delegate IDungeonResult Factory(
            IList<AccessibilityLevel> bossAccessibility, int accessible, bool sequenceBreak, bool visible);

        IList<AccessibilityLevel> BossAccessibility { get; }
        int Accessible { get; }
        bool Visible { get; }
        bool SequenceBreak { get; }
    }
}