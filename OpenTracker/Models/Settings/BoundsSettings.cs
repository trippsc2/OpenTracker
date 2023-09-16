namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains window bounds settings.
/// </summary>
public class BoundsSettings : IBoundsSettings
{
    public bool? Maximized { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
}