using System.Collections.Generic;
using Avalonia.Media;
using OpenTracker.Models.Accessibility;
using Reactive.Bindings;
using ReactiveUI;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This interface contains color settings data.
/// </summary>
public interface IColorSettings : IReactiveObject
{
    ReactiveProperty<SolidColorBrush> EmphasisFontColor { get; }
    Dictionary<AccessibilityLevel, ReactiveProperty<SolidColorBrush>> AccessibilityColors { get; }
    ReactiveProperty<SolidColorBrush> ConnectorColor { get; }
}