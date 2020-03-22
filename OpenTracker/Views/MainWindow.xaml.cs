using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views
{
    public class MainWindow : Window
    {
        public static AvaloniaProperty<Orientation> MapPanelOrientationProperty =
            AvaloniaProperty.Register<MainWindow, Orientation>("MapPanelOrientation");
        public Orientation MapPanelOrientation
        {
            get => GetValue(MapPanelOrientationProperty);
            set => SetValue(MapPanelOrientationProperty, value);
        }

        public static AvaloniaProperty<Dock> UIPanelDockProperty =
            AvaloniaProperty.Register<MainWindow, Dock>("UIPanelDock");
        public Dock UIPanelDock
        {
            get => GetValue(UIPanelDockProperty);
            set => SetValue(UIPanelDockProperty, value);
        }

        public static AvaloniaProperty<HorizontalAlignment> UIPanelHorizontalAlignmentProperty =
            AvaloniaProperty.Register<MainWindow, HorizontalAlignment>("UIPanelHorizontalAlignment");
        public HorizontalAlignment UIPanelHorizontalAlignment
        {
            get => GetValue(UIPanelHorizontalAlignmentProperty);
            set => SetValue(UIPanelHorizontalAlignmentProperty, value);
        }

        public static AvaloniaProperty<VerticalAlignment> UIPanelVerticalAlignmentProperty =
            AvaloniaProperty.Register<MainWindow, VerticalAlignment>("UIPanelVerticalAlignment");
        public VerticalAlignment UIPanelVerticalAlignment
        {
            get => GetValue(UIPanelVerticalAlignmentProperty);
            set => SetValue(UIPanelVerticalAlignmentProperty, value);
        }

        public static AvaloniaProperty<Dock> UIPanelOrientationDockProperty =
            AvaloniaProperty.Register<MainWindow, Dock>("UIPanelOrientationDock");
        public Dock UIPanelOrientationDock
        {
            get => GetValue(UIPanelOrientationDockProperty);
            set => SetValue(UIPanelOrientationDockProperty, value);
        }

        public static AvaloniaProperty<Thickness> PinnedLocationsPanelMarginProperty =
            AvaloniaProperty.Register<MainWindow, Thickness>("PinnedLocationsPanelMargin");
        public Thickness PinnedLocationsPanelMargin
        {
            get => GetValue(PinnedLocationsPanelMarginProperty);
            set => SetValue(PinnedLocationsPanelMarginProperty, value);
        }

        public static AvaloniaProperty<bool> ModeSettingsPopupOpenProperty =
            AvaloniaProperty.Register<MainWindow, bool>("ModeSettingsPopupOpen");
        public bool ModeSettingsPopupOpen
        {
            get => GetValue(ModeSettingsPopupOpenProperty);
            set => SetValue(ModeSettingsPopupOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AppSettingsPopupOpenProperty =
            AvaloniaProperty.Register<MainWindow, bool>("AppSettingsPopupOpen");
        public bool AppSettingsPopupOpen
        {
            get => GetValue(AppSettingsPopupOpenProperty);
            set => SetValue(AppSettingsPopupOpenProperty, value);
        }

        public MainWindow()
        {
            BoundsProperty.Changed.AddClassHandler<MainWindow>(BoundsChanged);
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void HorizontalLayout()
        {
            if (MapPanelOrientation != Orientation.Horizontal)
                MapPanelOrientation = Orientation.Horizontal;

            if (UIPanelDock != Dock.Bottom)
                UIPanelDock = Dock.Bottom;

            if (UIPanelHorizontalAlignment != HorizontalAlignment.Stretch)
                UIPanelHorizontalAlignment = HorizontalAlignment.Stretch;

            if (UIPanelVerticalAlignment != VerticalAlignment.Bottom)
                UIPanelVerticalAlignment = VerticalAlignment.Bottom;

            if (UIPanelOrientationDock != Dock.Left)
                UIPanelOrientationDock = Dock.Left;

            if (PinnedLocationsPanelMargin.Left != 2)
                PinnedLocationsPanelMargin = new Thickness(2, 0, 0, 0);
        }

        private void VerticalLayout()
        {
            if (MapPanelOrientation != Orientation.Vertical)
                MapPanelOrientation = Orientation.Vertical;

            if (UIPanelDock != Dock.Left)
                UIPanelDock = Dock.Left;

            if (UIPanelHorizontalAlignment != HorizontalAlignment.Right)
                UIPanelHorizontalAlignment = HorizontalAlignment.Right;

            if (UIPanelVerticalAlignment != VerticalAlignment.Stretch)
                UIPanelVerticalAlignment = VerticalAlignment.Stretch;

            if (UIPanelOrientationDock != Dock.Top)
                UIPanelOrientationDock = Dock.Top;

            if (PinnedLocationsPanelMargin.Left != 0)
                PinnedLocationsPanelMargin = new Thickness(0);
        }

        private void BoundsChanged(MainWindow window, AvaloniaPropertyChangedEventArgs e)
        {
            Rect bounds = (Rect)e.NewValue;

            if (bounds.Height >= bounds.Width && MapPanelOrientation != Orientation.Vertical)
                VerticalLayout();

            if (bounds.Width > bounds.Height)
                HorizontalLayout();
        }

        private void OpenModeSettingsPopup(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                ModeSettingsPopupOpen = true;
        }

        private void OpenAppSettingsPopup(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                AppSettingsPopupOpen = true;
        }
    }
}
