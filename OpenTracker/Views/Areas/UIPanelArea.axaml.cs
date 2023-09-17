using System;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Areas;
using ReactiveUI;

namespace OpenTracker.Views.Areas;

public sealed class UIPanelArea : ReactiveUserControl<UIPanelAreaVM>
{
    private ContentControl Dropdowns => this.FindControl<ContentControl>("Dropdowns");
    private ContentControl Dungeons => this.FindControl<ContentControl>("Dungeons");
    
    private ContentControl Items => this.FindControl<ContentControl>("Items");
    private ContentControl Locations => this.FindControl<ContentControl>("Locations");
    
    public UIPanelArea()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Dropdowns,
                    v => v.Dropdowns.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Dungeons,
                    v => v.Dungeons.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Items,
                    v => v.Items.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Locations,
                    v => v.Locations.Content)
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ViewModel!.ItemsDock)
                .Subscribe(x =>
                {
                    DockPanel.SetDock(Dropdowns, x);
                    DockPanel.SetDock(Dungeons, x);
                    DockPanel.SetDock(Items, x);
                })
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}