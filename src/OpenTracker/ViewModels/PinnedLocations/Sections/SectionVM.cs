using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Media;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using Reactive.Bindings;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This class contains the section control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class SectionVM : ViewModel, ISectionVM
{
    private ISection Section { get; }

    public string Name { get; }
    [ObservableAsProperty]
    private ReactiveProperty<SolidColorBrush> AccessibilityColor { get; } = default!;
    [ObservableAsProperty]
    public SolidColorBrush? FontColor { get; }
    [ObservableAsProperty]
    public bool Visible { get; }

    public List<ISectionIconVM> Icons { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    /// The color settings data.
    /// </param>
    /// <param name="section">
    /// The section to be represented.
    /// </param>
    /// <param name="icons">
    /// The observable collection of section icon control ViewModel instances.
    /// </param>
    public SectionVM(ColorSettings colorSettings, ISection section, List<ISectionIconVM> icons)
    {
        Section = section;
        Icons = icons;
        
        Name = Section.Name;

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Section.Accessibility)
                .Select(x => colorSettings.AccessibilityColors[x])
                .ToPropertyEx(this, x => x.AccessibilityColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Section.Accessibility,
                    x => x.AccessibilityColor,
                    x => x.AccessibilityColor.Value,
                    (accessibility, _, color) => accessibility != AccessibilityLevel.Normal ? color : null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.FontColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Section.IsActive)
                .ToPropertyEx(this, x => x.Visible)
                .DisposeWith(disposables);
        });
    }
}