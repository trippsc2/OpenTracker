using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains boss section data.
    /// </summary>
    public interface IBossSection : ISection
    {
        new AccessibilityLevel Accessibility { get; set; }

        IBossPlacement BossPlacement { get; }
    }
}