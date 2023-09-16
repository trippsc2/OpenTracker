using System.ComponentModel;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This class contains the logic for providing the section icon image.
/// </summary>
[DependencyInjection]
public sealed class SectionIconImageProvider : ViewModel, ISectionIconImageProvider
{
    private readonly ISection _section;
    private readonly string _imageSourceBase;

    public string ImageSource
    {
        get
        {
            if (!_section.IsAvailable())
            {
                return $"{_imageSourceBase}2.png";
            }

            switch (_section.Accessibility)
            {
                case AccessibilityLevel.None:
                case AccessibilityLevel.Inspect:
                    return $"{_imageSourceBase}0.png";
                case AccessibilityLevel.Partial:
                case AccessibilityLevel.SequenceBreak:
                case AccessibilityLevel.Normal:
                    return $"{_imageSourceBase}1.png";
                default:
                    return $"{_imageSourceBase}2.png";
            }
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="section">
    /// The section data.
    /// </param>
    /// <param name="imageSourceBase">
    /// A string representing the first portion of the image source.
    /// </param>
    public SectionIconImageProvider(ISection section, string imageSourceBase)
    {
        _section = section;
        _imageSourceBase = imageSourceBase;

        _section.PropertyChanged += OnSectionChanged;
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the ISection interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISection.Accessibility) ||
            e.PropertyName == nameof(ISection.Available))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        } 
    }
}