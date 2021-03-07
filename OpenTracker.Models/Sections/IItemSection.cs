namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This interface contains item section data.
    /// </summary>
    public interface IItemSection : ISection
    {
        int Total { get; }
        int Accessible { get; }
    }
}
