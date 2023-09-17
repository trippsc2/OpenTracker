using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Areas;
using ReactiveUI;

namespace OpenTracker.Views.Areas;

public sealed class MapArea : ReactiveUserControl<MapAreaVM>
{
    private ItemsControl Maps => this.FindControl<ItemsControl>("Maps");
    private ItemsControl Connectors => this.FindControl<ItemsControl>("Connectors");
    private ItemsControl MapLocations => this.FindControl<ItemsControl>("MapLocations");
    
    public MapArea()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            Maps.ItemsPanel = new FuncTemplate<IPanel>(() => new StackPanel
            {
                DataContext = DataContext,
                [!StackPanel.OrientationProperty] = new Binding("Orientation", BindingMode.OneWay)
            });
            Connectors.ItemsPanel = new FuncTemplate<IPanel>(() =>
                new Panel { Classes = new Classes("MapArea") });
            MapLocations.ItemsPanel = new FuncTemplate<IPanel>(() => new Canvas());

            Disposable
                .Create(() =>
                {
                    Maps.ItemsPanel = null;
                    Connectors.ItemsPanel = null;
                    MapLocations.ItemsPanel = null;
                })
                .DisposeWith(disposables);
            
            this.OneWayBind(ViewModel,
                    vm => vm.Maps,
                    v => v.Maps.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Connectors,
                    v => v.Connectors.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.MapLocations,
                    v => v.MapLocations.Items)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}