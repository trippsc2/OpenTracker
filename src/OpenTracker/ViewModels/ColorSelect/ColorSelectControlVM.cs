using Avalonia.Media;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using Reactive.Bindings;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.ColorSelect;

/// <summary>
/// This class contains the color select control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class ColorSelectControlVM : ViewModel
{
    public string Label { get; }
    
    public ReactiveProperty<SolidColorBrush> Color { get; }


    [Reactive]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool PickerOpen { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="color">
    /// A <see cref="ReactiveProperty{T}"/> of <see cref="SolidColorBrush"/> representing the color data.
    /// </param>
    /// <param name="label">
    /// A <see cref="string"/> representing the color label.
    /// </param>
    public ColorSelectControlVM(ReactiveProperty<SolidColorBrush> color, string label)
    {
        Color = color;
        Label = label;
    }
}