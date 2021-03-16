using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using OpenTracker.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.Views
{
    public class MainWindow : DialogWindowBase
    {
        private Orientation? _orientation;

        private new IMainWindowVM? ViewModel => DataContext as IMainWindowVM;

        public MainWindow()
        {
            BoundsProperty.Changed.AddClassHandler<MainWindow>(OnBoundsChanged);
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnBoundsChanged(MainWindow window, AvaloniaPropertyChangedEventArgs e)
        {
            ChangeLayout(Bounds);
        }

        private void OnClose(object? sender, CancelEventArgs e)
        {
            ViewModel!.OnClose(WindowState == WindowState.Maximized, Bounds, Position);
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (ViewModel?.X is null || ViewModel?.Y is null)
            {
                return;
            }

            if (!(GetScreen() is null))
            {
                Position = new PixelPoint((int)Math.Floor(ViewModel.X.Value), (int)Math.Floor(ViewModel.Y.Value));
            }

            if (!(ViewModel?.Maximized is null) && ViewModel.Maximized.Value)
            {
                WindowState = WindowState.Maximized;
            }
            
            ChangeLayout(Bounds);
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
}
