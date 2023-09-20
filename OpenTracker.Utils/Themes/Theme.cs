using Avalonia.Styling;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Utils.Themes;

/// <summary>
/// This class contains theme data.
/// </summary>
[DependencyInjection]
public sealed class Theme
{
    public required string Name { get; init; }
    public required IStyle Style { get; init; }
}