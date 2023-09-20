using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.UIPanels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Areas;

/// <summary>
/// This class contains the UI panel section control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class UIPanelAreaVM : ViewModel, IUIPanelAreaVM
{
    private ILayoutSettings LayoutSettings { get; }

    [ObservableAsProperty]
    public Dock ItemsDock { get; }

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
    public UIPanelAreaVM(ILayoutSettings layoutSettings, IUIPanelFactory factory)
    {
        LayoutSettings = layoutSettings;

        Dropdowns = factory.GetUIPanelVM(UIPanelType.Dropdown);
        Items = factory.GetUIPanelVM(UIPanelType.Item);
        Dungeons = factory.GetUIPanelVM(UIPanelType.Dungeon);
        Locations = factory.GetUIPanelVM(UIPanelType.Location);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.LayoutSettings.CurrentLayoutOrientation,
                    x => x.LayoutSettings.HorizontalItemsPlacement,
                    x => x.LayoutSettings.VerticalItemsPlacement,
                    (orientation, horizontal, vertical) =>
                        orientation == Orientation.Horizontal ? horizontal : vertical)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ItemsDock)
                .DisposeWith(disposables);
        });
    }
}