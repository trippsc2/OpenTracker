namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface providing access to the window bounds.
    /// </summary>
    public interface IBounds
    {
        bool? Maximized { get; set; }
        double? X { get; set; }
        double? Y { get; set; }
        double? Width { get; set; }
        double? Height { get; set; }
    }
}
