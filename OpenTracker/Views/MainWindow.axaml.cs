﻿using Avalonia;
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

        private IAutoTrackerAccess AutoTrackerAccess =>
            DataContext as IAutoTrackerAccess;
        private IBoundsData BoundsData =>
            DataContext as IBoundsData;
        private ICloseHandler CloseHandler =>
            DataContext as ICloseHandler;
        private IColorSelectAccess ColorSelectAccess =>
            DataContext as IColorSelectAccess;
        private IDynamicLayout DynamicLayout =>
            DataContext as IDynamicLayout;
        private IOpenData OpenData =>
            DataContext as IOpenData;
        private ISaveData SaveData =>
            DataContext as ISaveData;
        private ISequenceBreakAccess SequenceBreakAccess =>
            DataContext as ISequenceBreakAccess;

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

        private void OnBoundsChanged(MainWindow window, AvaloniaPropertyChangedEventArgs e)
        {
            ChangeLayout(Bounds);
        }

        private void OnClose(object sender, CancelEventArgs e)
        {
            CloseHandler.Close(WindowState == WindowState.Maximized, base.Bounds);

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

        private void OnDataContextChanged(object sender, EventArgs e)
        {
            if (BoundsData.Maximized.HasValue)
            {
                if (BoundsData.Maximized.Value)
                {
                    WindowState = WindowState.Maximized;
                }
            }

            if (BoundsData.X.HasValue && BoundsData.Y.HasValue &&
                BoundsData.Width.HasValue && BoundsData.Height.HasValue)
            {
                Bounds = new Rect(BoundsData.X.Value, BoundsData.Y.Value,
                    BoundsData.Width.Value, BoundsData.Height.Value);
            }

            ChangeLayout(Bounds);
        }

        private void ChangeLayout(Rect bounds)
        {
            Orientation orientation = bounds.Height >= bounds.Width ?
                Orientation.Vertical : Orientation.Horizontal;

            if (_orientation != orientation)
            {
                _orientation = orientation;
                DynamicLayout.ChangeLayout(orientation);
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
                    OpenData.Open(CurrentFilePath);
                })
                    .ConfigureAwait(false);
            }
        }

        public async Task Save()
        {
            if (CurrentFilePath != null)
            {
                SaveData.Save(CurrentFilePath);
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
                    SaveData.Save(CurrentFilePath);
                }).ConfigureAwait(false);
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
                    DataContext = AutoTrackerAccess.GetAutoTrackerViewModel()
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
                    DataContext = ColorSelectAccess.GetColorSelectViewModel()
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
                    DataContext = SequenceBreakAccess.GetSequenceBreakViewModel()
                };
                _sequenceBreakDialog.Show();
            }
        }
    }
}