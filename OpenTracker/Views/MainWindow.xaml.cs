using Avalonia;
using Avalonia.Controls;
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

        public static AvaloniaProperty<int> UIPanelColumnProperty =
            AvaloniaProperty.Register<MainWindow, int>("UIPanelColumn");
        public int UIPanelColumn
        {
            get => GetValue(UIPanelColumnProperty);
            set => SetValue(UIPanelColumnProperty, value);
        }

        public static AvaloniaProperty<int> UIPanelRowProperty =
            AvaloniaProperty.Register<MainWindow, int>("UIPanelRow");
        public int UIPanelRow
        {
            get => GetValue(UIPanelRowProperty);
            set => SetValue(UIPanelRowProperty, value);
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

        public static AvaloniaProperty<Dock> UIPanelDockProperty =
            AvaloniaProperty.Register<MainWindow, Dock>("UIPanelDock");
        public Dock UIPanelDock
        {
            get => GetValue(UIPanelDockProperty);
            set => SetValue(UIPanelDockProperty, value);
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

            if (UIPanelColumn != 1)
                UIPanelColumn = 1;

            if (UIPanelRow != 1)
                UIPanelRow = 1;

            if (UIPanelHorizontalAlignment != HorizontalAlignment.Stretch)
                UIPanelHorizontalAlignment = HorizontalAlignment.Stretch;

            if (UIPanelVerticalAlignment != VerticalAlignment.Bottom)
                UIPanelVerticalAlignment = VerticalAlignment.Bottom;

            if (UIPanelDock != Dock.Left)
                UIPanelDock = Dock.Left;
        }

        private void VerticalLayout()
        {
            if (MapPanelOrientation != Orientation.Vertical)
                MapPanelOrientation = Orientation.Vertical;

            if (UIPanelColumn != 0)
                UIPanelColumn = 0;

            if (UIPanelRow != 0)
                UIPanelRow = 0;

            if (UIPanelHorizontalAlignment != HorizontalAlignment.Right)
                UIPanelHorizontalAlignment = HorizontalAlignment.Right;

            if (UIPanelVerticalAlignment != VerticalAlignment.Stretch)
                UIPanelVerticalAlignment = VerticalAlignment.Stretch;

            if (UIPanelDock != Dock.Top)
                UIPanelDock = Dock.Top;
        }

        private void BoundsChanged(MainWindow window, AvaloniaPropertyChangedEventArgs e)
        {
            Rect bounds = (Rect)e.NewValue;

            if (bounds.Height >= bounds.Width && MapPanelOrientation != Orientation.Vertical)
                VerticalLayout();

            if (bounds.Width > bounds.Height)
                HorizontalLayout();
        }
    }
}
