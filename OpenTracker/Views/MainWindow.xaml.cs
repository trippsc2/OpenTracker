using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;
using Avalonia.Threading;
using OpenTracker.Interfaces;
using OpenTracker.Views.ColorSelect;
using OpenTracker.Views.SequenceBreaks;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.Views
{
    public class MainWindow : Window
    {
        private Orientation? _orientation;
        private AutoTrackerDialog _autoTrackerDialog;
        private ColorSelectDialog _colorSelectDialog;
        private SequenceBreakDialog _sequenceBreakDialog;

        public IAutoTrackerAccess ViewModelAutoTrackerAccess =>
            DataContext as IAutoTrackerAccess;
        public IBounds ViewModelBounds =>
            DataContext as IBounds;
        public IColorSelectAccess ViewModelColorSelectAccess =>
            DataContext as IColorSelectAccess;
        public IDynamicLayout ViewModelDynamicLayout =>
            DataContext as IDynamicLayout;
        public IOpen ViewModelOpen =>
            DataContext as IOpen;
        public ISave ViewModelSave =>
            DataContext as ISave;
        public ISaveAppSettings ViewModelSaveAppSettings =>
            DataContext as ISaveAppSettings;
        public ISequenceBreakAccess ViewModelSequenceBreakAccess =>
            DataContext as ISequenceBreakAccess;

        public static AvaloniaProperty<IThemeSelector> SelectorProperty =
            AvaloniaProperty.Register<MainWindow, IThemeSelector>(nameof(Selector));
        public IThemeSelector Selector
        {
            get => GetValue(SelectorProperty);
            set => SetValue(SelectorProperty, value);
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
            App.Selector.EnableThemes(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnDataContextChanged(object sender, EventArgs e)
        {
            if (ViewModelBounds.Maximized.HasValue)
            {
                if (ViewModelBounds.Maximized.Value)
                {
                    WindowState = WindowState.Maximized;
                }
            }

            if (ViewModelBounds.X.HasValue && ViewModelBounds.Y.HasValue &&
                ViewModelBounds.Width.HasValue && ViewModelBounds.Height.HasValue)
            {
                Bounds = new Rect(ViewModelBounds.X.Value, ViewModelBounds.Y.Value,
                    ViewModelBounds.Width.Value, ViewModelBounds.Height.Value);
            }

            ChangeLayout(Bounds);
        }

        private void OnBoundsChanged(MainWindow window, AvaloniaPropertyChangedEventArgs e)
        {
            ChangeLayout(Bounds);
        }

        private void ChangeLayout(Rect bounds)
        {
            Orientation orientation = bounds.Height >= bounds.Width ?
                Orientation.Vertical : Orientation.Horizontal;

            if (_orientation != orientation)
            {
                _orientation = orientation;
                ViewModelDynamicLayout.ChangeLayout(orientation);
            }
        }

        public async Task Save()
        {
            if (CurrentFilePath != null)
            {
                ViewModelSave.Save(CurrentFilePath);
            }
            else
            {
                await SaveAs().ConfigureAwait(false);
            }
        }

        public async Task SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "JSON", Extensions = { "json" } });

            string path = await dialog.ShowAsync(this).ConfigureAwait(false);

            if (path != null)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    CurrentFilePath = path;
                    ViewModelSave.Save(CurrentFilePath);
                }).ConfigureAwait(false);
            }
        }

        public async Task Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "JSON", Extensions = { "json" } });
            dialog.AllowMultiple = false;

            if (CurrentFilePath != null)
            {
                dialog.InitialFileName = CurrentFilePath;
            }

            string[] path = await dialog.ShowAsync(this).ConfigureAwait(false);

            if (path != null && path.Length > 0)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    CurrentFilePath = path[0];
                    ViewModelOpen.Open(CurrentFilePath);
                })
                    .ConfigureAwait(false);
            }
        }

        private void OnClose(object sender, CancelEventArgs e)
        {
            ViewModelSaveAppSettings.SaveAppSettings(WindowState == WindowState.Maximized, Bounds);

            if (_autoTrackerDialog != null && _autoTrackerDialog.IsVisible)
            {
                _autoTrackerDialog?.Close();
            }

            if (_colorSelectDialog != null && _colorSelectDialog.IsVisible)
            {
                _colorSelectDialog?.Close();
            }

            if (_sequenceBreakDialog != null && _sequenceBreakDialog.IsVisible)
            {
                _sequenceBreakDialog?.Close();
            }
        }

        public void AutoTracker()
        {
            if (_autoTrackerDialog != null && _autoTrackerDialog.IsVisible)
            {
                _autoTrackerDialog.Activate();
            }
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
            {
                _colorSelectDialog.Activate();
            }
            else
            {
                _colorSelectDialog = new ColorSelectDialog()
                {
                    DataContext = ViewModelColorSelectAccess.GetColorSelectViewModel()
                };
                _colorSelectDialog.Show();
            }
        }

        public void SequenceBreak()
        {
            if (_sequenceBreakDialog != null && _sequenceBreakDialog.IsVisible)
            {
                _sequenceBreakDialog.Activate();
            }
            else
            {
                _sequenceBreakDialog = new SequenceBreakDialog()
                {
                    DataContext = ViewModelSequenceBreakAccess.GetSequenceBreakViewModel()
                };
                _sequenceBreakDialog.Show();
            }
        }
    }
}
