using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Sections
{
    public interface IBossSection : ISection
    {
        new AccessibilityLevel Accessibility { get; set; }

        IBossPlacement BossPlacement { get; }
    }
}