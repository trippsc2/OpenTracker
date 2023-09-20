using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Sections;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This class contains the logic for providing the section icon image.
/// </summary>
[DependencyInjection]
public sealed class SectionIconImageProvider : ViewModel, ISectionIconImageProvider
{
    private readonly string _imageSourceBase;

    private ISection Section { get; }
    
    [ObservableAsProperty]
    public string ImageSource
    {
        get
        {
            if (!Section.IsAvailable())
            {
                return $"{_imageSourceBase}2.png";
            }

            switch (Section.Accessibility)
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
        _imageSourceBase = imageSourceBase;
        Section = section;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Section.Available,
                    x => x.Section.Accessibility,
                    GetImageSource)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }

    private string GetImageSource(int _, AccessibilityLevel accessibility)
    {
        if (!Section.IsAvailable())
        {
            return $"{_imageSourceBase}2.png";
        }
        
        return accessibility switch
        {
            AccessibilityLevel.None => $"{_imageSourceBase}0.png",
            AccessibilityLevel.Inspect => $"{_imageSourceBase}0.png",
            AccessibilityLevel.Partial => $"{_imageSourceBase}1.png",
            AccessibilityLevel.SequenceBreak => $"{_imageSourceBase}1.png",
            AccessibilityLevel.Normal => $"{_imageSourceBase}1.png",
            _ => $"{_imageSourceBase}2.png"
        };
    }
}