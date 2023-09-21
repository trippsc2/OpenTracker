using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains window bounds settings.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class BoundsSettings
{
    public bool? Maximized { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
}