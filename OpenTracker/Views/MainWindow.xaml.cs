using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;
using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.Views
{
    public class MainWindow : Window
    {
        private AutoTrackerDialog _autoTrackerDialog;
        private ColorSelectDialog _colorSelectDialog;

        public IAutoTrackerAccess ViewModelAutoTrackerAccess => DataContext as IAutoTrackerAccess;
        public IColorSelectAccess ViewModelColorSelectAccess => DataContext as IColorSelectAccess;
        public IOpen ViewModelOpen => DataContext as IOpen;
        public ISave ViewModelSave => DataContext as ISave;
        public ISaveAppSettings ViewModelSaveAppSettings => DataContext as ISaveAppSettings;
        private IAppSettings ViewModel => DataContext as IAppSettings;

        public static AvaloniaProperty<Dock> UIPanelDockProperty =
            AvaloniaProperty.Register<MainWindow, Dock>(nameof(UIPanelDock));
        public Dock UIPanelDock
        {
            get => GetValue(UIPanelDockProperty);
            set => SetValue(UIPanelDockProperty, value);
        }

        public static AvaloniaProperty<Avalonia.Layout.HorizontalAlignment> UIPanelHorizontalAlignmentProperty =
            AvaloniaProperty.Register<MainWindow, Avalonia.Layout.HorizontalAlignment>(nameof(UIPanelHorizontalAlignment));
        public Avalonia.Layout.HorizontalAlignment UIPanelHorizontalAlignment
        {
            get => GetValue(UIPanelHorizontalAlignmentProperty);
            set => SetValue(UIPanelHorizontalAlignmentProperty, value);
        }

        public static AvaloniaProperty<Avalonia.Layout.VerticalAlignment> UIPanelVerticalAlignmentProperty =
            AvaloniaProperty.Register<MainWindow, Avalonia.Layout.VerticalAlignment>(nameof(UIPanelVerticalAlignment));
        public Avalonia.Layout.VerticalAlignment UIPanelVerticalAlignment
        {
            get => GetValue(UIPanelVerticalAlignmentProperty);
            set => SetValue(UIPanelVerticalAlignmentProperty, value);
        }

        public static AvaloniaProperty<Dock> UIPanelOrientationDockProperty =
            AvaloniaProperty.Register<MainWindow, Dock>(nameof(UIPanelOrientationDock));
        public Dock UIPanelOrientationDock
        {
            get => GetValue(UIPanelOrientationDockProperty);
            set => SetValue(UIPanelOrientationDockProperty, value);
        }

        public static AvaloniaProperty<Thickness> ItemsPanelMarginProperty =
            AvaloniaProperty.Register<MainWindow, Thickness>(nameof(ItemsPanelMargin));
        public Thickness ItemsPanelMargin
        {
            get => GetValue(ItemsPanelMarginProperty);
            set => SetValue(ItemsPanelMarginProperty, value);
        }

        public static AvaloniaProperty<Thickness> LocationsPanelMarginProperty =
            AvaloniaProperty.Register<MainWindow, Thickness>(nameof(LocationsPanelMargin));
        public Thickness LocationsPanelMargin
        {
            get => GetValue(LocationsPanelMarginProperty);
            set => SetValue(LocationsPanelMarginProperty, value);
        }

        public static AvaloniaProperty<Orientation> MapPanelOrientationProperty =
            AvaloniaProperty.Register<MainWindow, Orientation>(nameof(MapPanelOrientation));
        public Orientation MapPanelOrientation
        {
            get => GetValue(MapPanelOrientationProperty);
            set => SetValue(MapPanelOrientationProperty, value);
        }

        public static AvaloniaProperty<Thickness> MapMarginProperty =
            AvaloniaProperty.Register<MainWindow, Thickness>(nameof(MapMargin));
        public Thickness MapMargin
        {
            get => GetValue(MapMarginProperty);
            set => SetValue(MapMarginProperty, value);
        }

        public static AvaloniaProperty<bool> ModeSettingsPopupOpenProperty =
            AvaloniaProperty.Register<MainWindow, bool>(nameof(ModeSettingsPopupOpen));
        public bool ModeSettingsPopupOpen
        {
            get => GetValue(ModeSettingsPopupOpenProperty);
            set => SetValue(ModeSettingsPopupOpenProperty, value);
        }

        public static AvaloniaProperty<string> CurrentFilePathProperty =
            AvaloniaProperty.Register<MainWindow, string>(nameof(CurrentFilePath));
        public string CurrentFilePath
        {
            get => GetValue(CurrentFilePathProperty);
            set => SetValue(CurrentFilePathProperty, value);
        }

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

        private void OnDataContextChanged(object sender, EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.PropertyChanged += OnViewModelChanged;

            UpdateLayoutOrientation();
        }

        private void OnViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAppSettings.LayoutOrientation) ||
                e.PropertyName == nameof(IAppSettings.MapOrientation) ||
                e.PropertyName == nameof(IAppSettings.HorizontalUIPanelPlacement) ||
                e.PropertyName == nameof(IAppSettings.VerticalUIPanelPlacement) ||
                e.PropertyName == nameof(IAppSettings.HorizontalItemsPlacement) ||
                e.PropertyName == nameof(IAppSettings.VerticalItemsPlacement))
                UpdateLayoutOrientation();
        }

        private void OnBoundsChanged(MainWindow window, AvaloniaPropertyChangedEventArgs e)
        {
            if (ViewModel.LayoutOrientation == LayoutOrientation.Dynamic)
                ChangeLayout((Rect)e.NewValue);
        }

        private void UpdateLayoutOrientation()
        {
            switch (ViewModel.LayoutOrientation)
            {
                case LayoutOrientation.Dynamic:
                    ChangeLayout(Bounds);
                    break;
                case LayoutOrientation.Horizontal:
                    HorizontalLayout();
                    break;
                case LayoutOrientation.Vertical:
                    VerticalLayout();
                    break;
            }
        }

        private void ChangeLayout(Rect bounds)
        {
            if (bounds.Height >= bounds.Width)
                VerticalLayout();

            if (bounds.Width > bounds.Height)
                HorizontalLayout();
        }

        private void HorizontalLayout()
        {
            if (ViewModel.HorizontalUIPanelPlacement == Models.Enums.VerticalAlignment.Top)
                UIPanelDock = Dock.Top;
            else
                UIPanelDock = Dock.Bottom;

            UIPanelHorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            UIPanelVerticalAlignment = Avalonia.Layout.VerticalAlignment.Bottom;

            if (ViewModel.MapOrientation == MapOrientation.Vertical)
            {
                MapPanelOrientation = Orientation.Vertical;
                MapMargin = new Thickness(20, 10);
            }
            else
            {
                MapPanelOrientation = Orientation.Horizontal;
                MapMargin = new Thickness(10, 20);
            }

            if (ViewModel.HorizontalItemsPlacement == Models.Enums.HorizontalAlignment.Left)
            {
                UIPanelOrientationDock = Dock.Left;
                ItemsPanelMargin = new Thickness(2, 0, 1, 2);
                LocationsPanelMargin = new Thickness(1, 0, 2, 2);
            }

            if (ViewModel.HorizontalItemsPlacement == Models.Enums.HorizontalAlignment.Right)
            {
                UIPanelOrientationDock = Dock.Right;
                ItemsPanelMargin = new Thickness(1, 0, 2, 2);
                LocationsPanelMargin = new Thickness(2, 0, 1, 2);
            }
        }

        private void VerticalLayout()
        {
            if (ViewModel.VerticalUIPanelPlacement == Models.Enums.HorizontalAlignment.Left)
                UIPanelDock = Dock.Left;
            else
                UIPanelDock = Dock.Right;

            UIPanelHorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right;
            UIPanelVerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;

            if (ViewModel.MapOrientation == MapOrientation.Horizontal)
            {
                MapPanelOrientation = Orientation.Horizontal;
                MapMargin = new Thickness(10, 20);
            }
            else
            {
                MapPanelOrientation = Orientation.Vertical;
                MapMargin = new Thickness(20, 10);
            }

            if (ViewModel.VerticalItemsPlacement == Models.Enums.VerticalAlignment.Top)
            {
                UIPanelOrientationDock = Dock.Top;
                ItemsPanelMargin = new Thickness(2, 2, 0, 1);
                LocationsPanelMargin = new Thickness(2, 1, 0, 2);
            }

            if (ViewModel.VerticalItemsPlacement == Models.Enums.VerticalAlignment.Bottom)
            {
                UIPanelOrientationDock = Dock.Bottom;
                ItemsPanelMargin = new Thickness(2, 1, 0, 2);
                LocationsPanelMargin = new Thickness(2, 2, 0, 1);
            }
        }

        public async Task Save()
        {
            if (CurrentFilePath != null)
                ViewModelSave.Save(CurrentFilePath);
            else
                await SaveAs();
        }

        public async Task SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "JSON", Extensions = { "json" } });

            string path = await dialog.ShowAsync(this);

            CurrentFilePath = path;

            ViewModelSave.Save(CurrentFilePath);
        }

        public async Task Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "JSON", Extensions = { "json" } });
            dialog.AllowMultiple = false;

            if (CurrentFilePath != null)
                dialog.InitialFileName = CurrentFilePath;

            string[] path = await dialog.ShowAsync(this);

            CurrentFilePath = path[0];

            ViewModelOpen.Open(CurrentFilePath);
        }

        private void OpenModeSettingsPopup(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                ModeSettingsPopupOpen = true;
        }

        private void OnClose(object sender, CancelEventArgs e)
        {
            ViewModelSaveAppSettings.SaveAppSettings();

            if (_autoTrackerDialog != null && _autoTrackerDialog.IsVisible)
                _autoTrackerDialog?.Close();

            if (_colorSelectDialog != null && _colorSelectDialog.IsVisible)
                _colorSelectDialog?.Close();
        }

        public void AutoTracker()
        {
            if (_autoTrackerDialog != null && _autoTrackerDialog.IsVisible)
                _autoTrackerDialog.Activate();
            else
            {
                _autoTrackerDialog = new AutoTrackerDialog()
                {
                    DataContext = ViewModelAutoTrackerAccess.GetAutoTrackerViewModel()
                };
                _autoTrackerDialog.Show();
            }
        }

        public void ColorSelect()
        {
            if (_colorSelectDialog != null && _colorSelectDialog.IsVisible)
                _colorSelectDialog.Activate();
            else
            {
                _colorSelectDialog = new ColorSelectDialog() { DataContext = ViewModelColorSelectAccess.GetColorSelectViewModel() };
                _colorSelectDialog.Show();
            }
        }
    }
}
