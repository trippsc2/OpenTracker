namespace OpenTracker.Models.Sections
{
    public interface IDungeonItemSection : IItemSection
    {
        new AccessibilityLevel Accessibility { get; set; }
        new int Accessible { get; set; }
    }
}
