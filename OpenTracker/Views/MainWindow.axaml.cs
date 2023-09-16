using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using OpenTracker.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace OpenTracker.Views;

public sealed class MainWindow : ReactiveWindow<MainWindowVM>
{
    private Orientation? _orientation;

    private ContentControl TopMenu => this.FindControl<ContentControl>("TopMenu");
    private ContentControl StatusBar => this.FindControl<ContentControl>("StatusBar");
    private ContentControl UIPanel => this.FindControl<ContentControl>("UIPanel");
    private ContentControl MapArea => this.FindControl<ContentControl>("MapArea");

    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        var savedBoundsLoaded = false;
        
        this.WhenActivated(disposables =>
        {
            if (!savedBoundsLoaded)
            {
                LoadSavedWindowSizeAndPosition();
                savedBoundsLoaded = true;
            }

            this.OneWayBind(ViewModel,
                    vm => vm.Title,
                    v => v.Title)
                .DisposeWith(disposables);
            this.Bind(ViewModel,
                    vm => vm.Height,
                    v => v.Height)
                .DisposeWith(disposables);
            this.Bind(ViewModel,
                    vm => vm.Width,
                    v => v.Width)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    vm => vm.TopMenu,
                    v => v.TopMenu.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.StatusBar,
                    v => v.StatusBar.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.UIPanel,
                    v => v.UIPanel.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.MapArea,
                    v => v.MapArea.Content)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.ViewModel!.UIDock)
                .Subscribe(x => DockPanel.SetDock(UIPanel, x))
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.Bounds)
                .Subscribe(ChangeLayout)
                .DisposeWith(disposables);
        });
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        ViewModel?.OnClose(WindowState == WindowState.Maximized, Bounds, Position);

        base.OnClosing(e);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void LoadSavedWindowSizeAndPosition()
    {
        if (ViewModel?.X is null || ViewModel?.Y is null)
        {
            return;
        }

        if (GetScreen() is not null)
        {
            Position = new PixelPoint((int)Math.Floor(ViewModel.X.Value), (int)Math.Floor(ViewModel.Y.Value));
        }

        if (ViewModel?.Maximized is not null && ViewModel.Maximized.Value)
        {
            WindowState = WindowState.Maximized;
        }
    }

    private Screen? GetScreen()
    {
        if (ViewModel?.X is null || ViewModel?.Y is null)
        {
            return null;
        }

        return Screens.All.FirstOrDefault(
            screen => screen.Bounds.X <= ViewModel.X.Value && screen.Bounds.Y <= ViewModel.Y.Value &&
                      screen.Bounds.X + screen.Bounds.Width > ViewModel.X.Value &&
                      screen.Bounds.Y + screen.Bounds.Height > ViewModel.Y.Value);
    }

    private void ChangeLayout(Rect bounds)
    {
        var orientation = bounds.Height >= bounds.Width ? Orientation.Vertical : Orientation.Horizontal;

        if (_orientation == orientation)
        {
            return;
        }
            
        _orientation = orientation;

        ViewModel?.ChangeLayout(orientation);
    }
}