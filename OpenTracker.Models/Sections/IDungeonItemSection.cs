using OpenTracker.Models.AccessibilityLevels;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the interface for dungeon item sections.
    /// </summary>
    public interface IDungeonItemSection : IItemSection
    {
        new AccessibilityLevel Accessibility { get; set; }
        new int Accessible { get; set; }
    }
}
