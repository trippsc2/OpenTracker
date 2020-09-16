using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the interface for boss sections.
    /// </summary>
    public interface IBossSection : ISection
    {
        new AccessibilityLevel Accessibility { get; set; }

        IBossPlacement BossPlacement { get; }
    }
}