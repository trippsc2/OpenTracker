using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Dungeons;

[DependencyInjection]
public sealed class DungeonPanelVM : ViewModel, IDungeonPanelVM
{
    private ILayoutSettings LayoutSettings { get; }

    [ObservableAsProperty]
    public Orientation Orientation { get; }
    [ObservableAsProperty]
    public IOrientedDungeonPanelVMBase Items { get; } = default!;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="layoutSettings">
    /// The layout settings data.
    /// </param>
    /// <param name="horizontalSmallItemPanel">
    /// The horizontal small item panel control.
    /// </param>
    /// <param name="verticalSmallItemPanel">
    /// The vertical small item panel control.
    /// </param>
    public DungeonPanelVM(
        ILayoutSettings layoutSettings,
        IHorizontalDungeonPanelVM horizontalSmallItemPanel,
        IVerticalDungeonPanelVM verticalSmallItemPanel)
    {
        LayoutSettings = layoutSettings;
        
        this.WhenActivated(disposables =>
        {
            var layoutOrientation = this
                .WhenAnyValue(x => x.LayoutSettings.CurrentLayoutOrientation);
                
            layoutOrientation
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Orientation)
                .DisposeWith(disposables);

            layoutOrientation
                .Select<Orientation, IOrientedDungeonPanelVMBase>(x =>
                    x == Orientation.Horizontal
                        ? horizontalSmallItemPanel
                        : verticalSmallItemPanel)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Items)
                .DisposeWith(disposables);
        });
    }
}