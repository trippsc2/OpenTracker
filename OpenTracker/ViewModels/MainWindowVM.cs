using System.ComponentModel;
using System.Reactive;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.Utils.Dialog;
using OpenTracker.ViewModels.Areas;
using OpenTracker.ViewModels.Menus;
using ReactiveUI;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This class contains the main window ViewModel data.
    /// </summary>
    public class MainWindowVM : DialogViewModelBase, IMainWindowVM
    {
        private readonly IAppSettings _appSettings;
        private readonly IBoundsSettings _boundsSettings;
        private readonly IJsonConverter _jsonConverter;
        private readonly ILayoutSettings _layoutSettings;
        private readonly ISaveLoadManager _saveLoadManager;

        public string Title
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("OpenTracker - ");
                sb.Append(_saveLoadManager.CurrentFilePath ?? "Unsaved");

                if (_saveLoadManager.Unsaved)
                {
                    sb.Append('*');
                }

                return sb.ToString();
            }
        }

        public bool? Maximized
        {
            get => _boundsSettings.Maximized;
            set => _boundsSettings.Maximized = value;
        }
        public double? X
        {
            get => _boundsSettings.X;
            set => _boundsSettings.X = value;
        }
        public double? Y
        {
            get => _boundsSettings.Y;
            set => _boundsSettings.Y = value;
        }
        public double? Width
        {
            get => _boundsSettings.Width;
            set => _boundsSettings.Width = value;
        }
        public double? Height
        {
            get => _boundsSettings.Height;
            set => _boundsSettings.Height = value;
        }

        public Dock UIDock => _layoutSettings.CurrentLayoutOrientation switch
        {
            Orientation.Horizontal => _layoutSettings.HorizontalUIPanelPlacement,
            _ => _layoutSettings.VerticalUIPanelPlacement
        };

        public ITopMenuVM TopMenu { get; }
        public IStatusBarVM StatusBar { get; }
        public IUIPanelAreaVM UIPanel { get; }
        public IMapAreaVM MapArea { get; }

        public ReactiveCommand<Unit, Unit> Open { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public ReactiveCommand<Unit, Unit> SaveAs { get; }
        public ReactiveCommand<Unit, Unit> Reset { get; }
        public ReactiveCommand<Unit, Unit> Undo { get; }
        public ReactiveCommand<Unit, Unit> Redo { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings">
        /// The app settings data.
        /// </param>
        /// <param name="boundsSettings">
        /// The bounds settings data.
        /// </param>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="jsonConverter">
        ///     The JSON converter.
        /// </param>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="topMenu">
        /// The top menu control.
        /// </param>
        /// <param name="statusBar">
        /// The status bar control.
        /// </param>
        /// <param name="uiPanel">
        /// The UI panel control.
        /// </param>
        /// <param name="mapArea">
        /// The map area control.
        /// </param>
        public MainWindowVM(
            IAppSettings appSettings, IBoundsSettings boundsSettings, ILayoutSettings layoutSettings,
            IJsonConverter jsonConverter, ISaveLoadManager saveLoadManager, ITopMenuVM.Factory topMenu,
            IStatusBarVM statusBar, IUIPanelAreaVM uiPanel, IMapAreaVM mapArea)
        {
            _appSettings = appSettings;
            _boundsSettings = boundsSettings;
            _layoutSettings = layoutSettings;
            _saveLoadManager = saveLoadManager;

            TopMenu = topMenu(() => Close());
            StatusBar = statusBar;
            UIPanel = uiPanel;
            MapArea = mapArea;
            _jsonConverter = jsonConverter;

            Open = TopMenu.Open;
            Save = TopMenu.Save;
            SaveAs = TopMenu.SaveAs;
            Reset = TopMenu.Reset;
            Undo = TopMenu.Undo;
            Redo = TopMenu.Redo;
            ToggleDisplayAllLocations = TopMenu.ToggleDisplayAllLocations;

            _layoutSettings.PropertyChanged += OnLayoutChanged;
            _saveLoadManager.PropertyChanged += OnSaveLoadManagerChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation) ||
                e.PropertyName == nameof(LayoutSettings.HorizontalUIPanelPlacement) ||
                e.PropertyName == nameof(LayoutSettings.VerticalUIPanelPlacement))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(UIDock)));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISaveLoadManager interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnSaveLoadManagerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISaveLoadManager.CurrentFilePath) ||
                e.PropertyName == nameof(ISaveLoadManager.Unsaved))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Title)));
            }
        }

        /// <summary>
        /// Changes the expected orientation layout, if dynamic orientation is enabled,
        /// to the specified orientation.
        /// </summary>
        /// <param name="orientation">
        /// The new expected orientation layout.
        /// </param>
        public void ChangeLayout(Orientation orientation)
        {
            _layoutSettings.CurrentDynamicOrientation = orientation;
        }

        /// <summary>
        /// Handles closing the app.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        /// <param name="pixelPoint">
        /// The pixel point representing the current window position.
        /// </param>
        public void OnClose(bool maximized, Rect bounds, PixelPoint pixelPoint)
        {
            SaveAppSettings(maximized, bounds, pixelPoint);
        }

        /// <summary>
        /// Saves the app settings to a file.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        /// <param name="pixelPoint">
        /// The pixel point representing the current window position.
        /// </param>
        private void SaveAppSettings(bool maximized, Rect bounds, PixelPoint pixelPoint)
        {
            _boundsSettings.Maximized = maximized;
            _boundsSettings.X = pixelPoint.X;
            _boundsSettings.Y = pixelPoint.Y;
            _boundsSettings.Width = bounds.Width;
            _boundsSettings.Height = bounds.Height;

            _jsonConverter.Save(_appSettings.Save(), AppPath.AppSettingsFilePath);
        }
    }
}
