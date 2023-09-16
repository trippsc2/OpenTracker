using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.UIPanels;
using ReactiveUI;

namespace OpenTracker.ViewModels.Areas;

/// <summary>
/// This class contains the UI panel section control ViewModel data.
/// </summary>
public class UIPanelAreaVM : ViewModelBase, IUIPanelAreaVM
{
    private readonly ILayoutSettings _layoutSettings;
    public Dock ItemsDock => _layoutSettings.CurrentLayoutOrientation switch
    {
        Orientation.Horizontal => _layoutSettings.HorizontalItemsPlacement,
        _ => _layoutSettings.VerticalItemsPlacement
    };

    public IUIPanelVM Dropdowns { get; }
    public IUIPanelVM Items { get; }
    public IUIPanelVM Dungeons { get; }
    public IUIPanelVM Locations { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings.
    /// </param>
    /// <param name="factory">
    /// A factory for creating UI panel controls.
    /// </param>
    public UIPanelAreaVM(
        ILayoutSettings layoutSettings, IUIPanelFactory factory)
    {
        _layoutSettings = layoutSettings;

        Dropdowns = factory.GetUIPanelVM(UIPanelType.Dropdown);
        Items = factory.GetUIPanelVM(UIPanelType.Item);
        Dungeons = factory.GetUIPanelVM(UIPanelType.Dungeon);
        Locations = factory.GetUIPanelVM(UIPanelType.Location);

        _layoutSettings.PropertyChanged += OnLayoutChanged;
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
    private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation) ||
            e.PropertyName == nameof(ILayoutSettings.HorizontalItemsPlacement) ||
            e.PropertyName == nameof(ILayoutSettings.VerticalItemsPlacement))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ItemsDock)));
        }
    }
}