using System.ComponentModel;
using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.UIPanels;

/// <summary>
/// This class contains the UI panel control ViewModel data.
/// </summary>
public class UIPanelVM : ViewModelBase, IUIPanelVM
{
    private readonly ILayoutSettings _layoutSettings;
    private readonly IRequirement? _requirement;

    public bool Visible => _requirement is null || _requirement.Met;
    public double Scale => _layoutSettings.UIScale;

    public string Title { get; }
    public IModeSettingsVM? ModeSettings { get; }
    public bool AlternateBodyColor { get; }
    public IUIPanelBodyVMBase Body { get; }

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
        ILayoutSettings layoutSettings, IRequirement? requirement, string title, IModeSettingsVM? modeSettings,
        bool alternateBodyColor, IUIPanelBodyVMBase body)
    {
        _layoutSettings = layoutSettings;
        _requirement = requirement;
            
        Title = title;
        ModeSettings = modeSettings;
        AlternateBodyColor = alternateBodyColor;
        Body = body;

        _layoutSettings.PropertyChanged += OnLayoutSettingsChanged;

        if (_requirement is not null)
        {
            _requirement.PropertyChanged += OnRequirementChanged;
        }
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnLayoutSettingsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.UIScale))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
        }
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the IRequirement interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Met))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
        }
    }
}