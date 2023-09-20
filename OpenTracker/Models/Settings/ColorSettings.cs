using System.Collections.Generic;
using Avalonia.Media;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils.Autofac;
using Reactive.Bindings;
using ReactiveUI;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains color settings data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ColorSettings : ReactiveObject, IColorSettings
{
    public ReactiveProperty<SolidColorBrush> EmphasisFontColor { get; } =
        new() { Value = SolidColorBrush.Parse("#00ff00") };
    
    public Dictionary<AccessibilityLevel, ReactiveProperty<SolidColorBrush>> AccessibilityColors { get; } =
        new()
        {
            { AccessibilityLevel.None, new ReactiveProperty<SolidColorBrush> {Value = SolidColorBrush.Parse("#ff3030")} },
            { AccessibilityLevel.Inspect, new ReactiveProperty<SolidColorBrush> {Value = SolidColorBrush.Parse("#6495ed")} },
            { AccessibilityLevel.Partial, new ReactiveProperty<SolidColorBrush> {Value = SolidColorBrush.Parse("#ff8c00")} },
            { AccessibilityLevel.SequenceBreak, new ReactiveProperty<SolidColorBrush> {Value = SolidColorBrush.Parse("#ffff00")} },
            { AccessibilityLevel.Normal, new ReactiveProperty<SolidColorBrush> {Value = SolidColorBrush.Parse("#00ff00")} },
            { AccessibilityLevel.Cleared, new ReactiveProperty<SolidColorBrush> {Value = SolidColorBrush.Parse("#ff333333")} }
        };
    
    public ReactiveProperty<SolidColorBrush> ConnectorColor { get; } =
        new() { Value = SolidColorBrush.Parse("#40e0d0") };
}