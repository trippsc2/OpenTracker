using OpenTracker.Models.AccessibilityLevels;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains dungeon item section data.
    /// </summary>
    public interface IDungeonItemSection : IItemSection
    {
        new AccessibilityLevel Accessibility { get; set; }
        new int Accessible { get; set; }
    }
}
