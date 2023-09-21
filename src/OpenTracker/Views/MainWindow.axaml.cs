using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using OpenTracker.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace OpenTracker.Views;

public sealed class MainWindow : ReactiveWindow<MainWindowVM>
{
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

            if (ViewModel is null)
            {
                return;
            }
            
            ViewModel!.RequestCloseInteraction.RegisterHandler(interaction =>
                {
                    interaction.SetOutput(Unit.Default);
                    Close(interaction.Input);
                })
                .DisposeWith(disposables);
            
            this.WhenAnyValue(x => x.Bounds)
                .InvokeCommand(ViewModel.ChangeLayoutCommand)
                .DisposeWith(disposables);

            var keyDownObservable = this.Events()
                .KeyDown;

            keyDownObservable
                .Where(x => x.Key == Key.O && x.KeyModifiers == KeyModifiers.Control)
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.OpenCommand)
                .DisposeWith(disposables);
            keyDownObservable
                .Where(x => x.Key == Key.S && x.KeyModifiers == KeyModifiers.Control)
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.SaveCommand)
                .DisposeWith(disposables);
            keyDownObservable
                .Where(x => x.Key == Key.S && x.KeyModifiers == (KeyModifiers.Control | KeyModifiers.Shift))
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.SaveAsCommand)
                .DisposeWith(disposables);
            keyDownObservable
                .Where(x => x.Key == Key.Z && x.KeyModifiers == KeyModifiers.Control)
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.UndoCommand)
                .DisposeWith(disposables);
            keyDownObservable
                .Where(x => x.Key == Key.Y && x.KeyModifiers == KeyModifiers.Control)
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.RedoCommand)
                .DisposeWith(disposables);
            keyDownObservable
                .Where(x => x.Key == Key.F5)
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.ResetCommand)
                .DisposeWith(disposables);
            keyDownObservable
                .Where(x => x.Key == Key.F11)
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel.ToggleDisplayAllLocationsCommand)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        ViewModel?.OnClose(WindowState == WindowState.Maximized, Bounds, Position);

        base.OnClosing(e);
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
}