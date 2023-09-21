using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.UIPanels;

/// <summary>
/// This class contains the UI panel control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class UIPanelVM : ViewModel, IUIPanelVM
{
    private LayoutSettings LayoutSettings { get; }
    private IRequirement? Requirement { get; }
    public string Title { get; }
    public IModeSettingsVM? ModeSettings { get; }
    public bool AlternateBodyColor { get; }
    public IViewModel Body { get; }

    [ObservableAsProperty]
    public bool Visible { get; }
    [ObservableAsProperty]
    public double Scale { get; }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="requirement">
    /// The requirement for the UI panel to be visible.
    /// </param>
    /// <param name="title">
    /// A string representing the title of the UI panel.
    /// </param>
    /// <param name="modeSettings">
    /// A nullable property for the mode settings.
    /// </param>
    /// <param name="alternateBodyColor">
    /// A boolean representing whether to use the alternate body color.
    /// </param>
    /// <param name="body">
    /// The body control of the panel.
    /// </param>
    public UIPanelVM(
        LayoutSettings layoutSettings,
        IRequirement? requirement,
        string title,
        IModeSettingsVM? modeSettings,
        bool alternateBodyColor,
        IViewModel body)
    {
        LayoutSettings = layoutSettings;
        Requirement = requirement;
        Title = title;
        ModeSettings = modeSettings;
        AlternateBodyColor = alternateBodyColor;
        Body = body;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Requirement,
                    x => x.Requirement!.Met,
                    (x, _) => x?.Met ?? true)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible, initialValue: true)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.LayoutSettings.UIScale)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Scale)
                .DisposeWith(disposables);
        });
    }
}