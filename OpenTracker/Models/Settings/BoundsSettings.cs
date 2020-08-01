namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the class containing the saved window bounds.
    /// </summary>
    public class BoundsSettings
    {
        public bool? Maximized { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
    }
}
