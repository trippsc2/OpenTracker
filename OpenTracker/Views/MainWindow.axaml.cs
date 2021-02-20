using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using OpenTracker.ViewModels;
using System;
using System.ComponentModel;

namespace OpenTracker.Views
{
    public class MainWindow : Window
    {
        private Orientation? _orientation;

        private IMainWindowVM? ViewModel =>
            DataContext as IMainWindowVM;

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

        private void OnClose(object sender, CancelEventArgs e)
        {
            _ = ViewModel ?? throw new NullReferenceException();

            ViewModel.Close(WindowState == WindowState.Maximized, Bounds, Position);
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (ViewModel == null || !ViewModel.X.HasValue || !ViewModel.Y.HasValue)
            {
                return;
            }

            if (GetScreen() != null)
            {
                Position = new PixelPoint(
                    (int)Math.Floor(ViewModel.X.Value), (int)Math.Floor(ViewModel.Y.Value));
            }

            if (ViewModel.Maximized.HasValue)
            {
                if (ViewModel.Maximized.Value)
                {
                    WindowState = WindowState.Maximized;
                }
            }

            ChangeLayout(Bounds);
        }

        private Screen? GetScreen()
        {
            if (ViewModel == null || !ViewModel.X.HasValue || !ViewModel.Y.HasValue)
            {
                return null;
            }

            foreach (var screen in Screens.All)
            {
                if (screen.Bounds.X <= ViewModel.X.Value &&
                    screen.Bounds.Y <= ViewModel.Y.Value &&
                    screen.Bounds.X + screen.Bounds.Width > ViewModel.X.Value &&
                    screen.Bounds.Y + screen.Bounds.Height > ViewModel.Y.Value)
                {
                    return screen;
                }
            }

            return null;
        }

        private void ChangeLayout(Rect bounds)
        {
            Orientation orientation = bounds.Height >= bounds.Width ?
                Orientation.Vertical : Orientation.Horizontal;

            if (_orientation != orientation)
            {
                _orientation = orientation;
                
                if (ViewModel == null)
                {
                    return;
                }

                ViewModel.ChangeLayout(orientation);
            }
        }
    }
}
