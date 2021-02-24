using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Newtonsoft.Json;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Maps;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Text;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the class for the main window ViewModel.
    /// </summary>
    public class MainWindowVM : ViewModelBase, IMainWindowVM
    {
        private readonly IAppSettings _appSettings;
        private readonly ISaveLoadManager _saveLoadManager;
        private readonly ISequenceBreakDictionary _sequenceBreaks;

        public string Title
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("OpenTracker - ");

                if (_saveLoadManager.CurrentFilePath == null)
                {
                    sb.Append("Untitled");
                }
                else
                {
                    sb.Append(_saveLoadManager.CurrentFilePath);
                }

                if (_saveLoadManager.Unsaved)
                {
                    sb.Append('*');
                }

                return sb.ToString();
            }
        }

        public bool? Maximized
        {
            get => _appSettings.Bounds.Maximized;
            set => _appSettings.Bounds.Maximized = value;
        }
        public double? X
        {
            get => _appSettings.Bounds.X;
            set => _appSettings.Bounds.X = value;
        }
        public double? Y
        {
            get => _appSettings.Bounds.Y;
            set => _appSettings.Bounds.Y = value;
        }
        public double? Width
        {
            get => _appSettings.Bounds.Width;
            set => _appSettings.Bounds.Width = value;
        }
        public double? Height
        {
            get => _appSettings.Bounds.Height;
            set => _appSettings.Bounds.Height = value;
        }

        public Dock UIDock =>
            _appSettings.Layout.CurrentLayoutOrientation switch
            {
                Orientation.Horizontal => _appSettings.Layout.HorizontalUIPanelPlacement,
                _ => _appSettings.Layout.VerticalUIPanelPlacement
            };

        public ITopMenuVM TopMenu { get; }
        public IStatusBarVM StatusBar { get; }
        public IUIPanelVM UIPanel { get; }
        public IMapAreaVM MapArea { get; }

        public ReactiveCommand<Unit, Unit> ResetCommand =>
            TopMenu.ResetCommand;
        public ReactiveCommand<Unit, Unit> UndoCommand =>
            TopMenu.UndoCommand;
        public ReactiveCommand<Unit, Unit> RedoCommand =>
            TopMenu.RedoCommand;
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand =>
            TopMenu.ToggleDisplayAllLocationsCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM(
            IAppSettings appSettings, ISaveLoadManager saveLoadManager,
            ISequenceBreakDictionary sequenceBreaks, ITopMenuVM topMenu, IStatusBarVM statusBar,
            IUIPanelVM uiPanel, IMapAreaVM mapArea)
        {
            _appSettings = appSettings;
            _saveLoadManager = saveLoadManager;
            _sequenceBreaks = sequenceBreaks;

            TopMenu = topMenu;
            StatusBar = statusBar;
            UIPanel = uiPanel;
            MapArea = mapArea;

            _saveLoadManager.PropertyChanged += OnSaveLoadManagerChanged;
            _appSettings.Layout.PropertyChanged += OnLayoutChanged;
            LoadSequenceBreaks();
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
        private void OnSaveLoadManagerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISaveLoadManager.CurrentFilePath) ||
                e.PropertyName == nameof(ISaveLoadManager.Unsaved))
            {
                this.RaisePropertyChanged(nameof(Title));
            }
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
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation) ||
                e.PropertyName == nameof(LayoutSettings.HorizontalUIPanelPlacement) ||
                e.PropertyName == nameof(LayoutSettings.VerticalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(UIDock));
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
            _appSettings.Layout.CurrentDynamicOrientation = orientation;
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
        private void SaveAppSettings(bool maximized, Rect bounds, PixelPoint pixelPoint)
        {
            _appSettings.Bounds.Maximized = maximized;
            _appSettings.Bounds.X = pixelPoint.X;
            _appSettings.Bounds.Y = pixelPoint.Y;
            _appSettings.Bounds.Width = bounds.Width;
            _appSettings.Bounds.Height = bounds.Height;

            JsonConversion.Save(_appSettings.Save(), AppPath.AppSettingsFilePath);
        }

        /// <summary>
        /// Saves the sequence break settings to a file.
        /// </summary>
        private void SaveSequenceBreaks()
        {
            JsonConversion.Save(_sequenceBreaks.Save(), AppPath.SequenceBreakPath);
        }

        /// <summary>
        /// Loads the sequence break settings from a file.
        /// </summary>
        private void LoadSequenceBreaks()
        {
            var saveData = JsonConversion
                .Load<Dictionary<SequenceBreakType, SequenceBreakSaveData>>(
                AppPath.SequenceBreakPath);

            if (saveData != null)
            {
                _sequenceBreaks.Load(saveData);
            }
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
        public void Close(bool maximized, Rect bounds, PixelPoint pixelPoint)
        {
            SaveAppSettings(maximized, bounds, pixelPoint);
            SaveSequenceBreaks();
        }
    }
}
