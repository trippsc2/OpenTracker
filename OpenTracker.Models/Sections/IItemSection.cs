namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the interface for item sections.
    /// </summary>
    public interface IItemSection : ISection
    {
        int Total { get; }
        int Accessible { get; }
    }
}
