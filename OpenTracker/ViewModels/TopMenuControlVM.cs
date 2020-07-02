using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class TopMenuControlVM : ViewModelBase
    {
        private readonly AppSettings _appSettings;

        public bool DisplayAllLocations =>
            _appSettings.DisplayAllLocations;
        public bool ShowItemCountsOnMap =>
            _appSettings.ShowItemCountsOnMap;

        public bool DynamicLayoutOrientation =>
            _appSettings.LayoutOrientation == null;
        public bool HorizontalLayoutOrientation =>
            _appSettings.LayoutOrientation == Orientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            _appSettings.LayoutOrientation == Orientation.Vertical;

        public bool DynamicMapOrientation =>
            _appSettings.MapOrientation == null;
        public bool HorizontalMapOrientation =>
            _appSettings.MapOrientation == Orientation.Horizontal;
        public bool VerticalMapOrientation =>
            _appSettings.MapOrientation == Orientation.Vertical;

        public bool TopHorizontalUIPanelPlacement =>
            _appSettings.HorizontalUIPanelPlacement == VerticalAlignment.Top;
        public bool BottomHorizontalUIPanelPlacement =>
            _appSettings.HorizontalUIPanelPlacement == VerticalAlignment.Bottom;

        public bool LeftVerticalUIPanelPlacement =>
            _appSettings.VerticalUIPanelPlacement == HorizontalAlignment.Left;
        public bool RightVerticalUIPanelPlacement =>
            _appSettings.VerticalUIPanelPlacement == HorizontalAlignment.Right;

        public bool LeftHorizontalItemsPlacement =>
            _appSettings.HorizontalItemsPlacement == HorizontalAlignment.Left;
        public bool RightHorizontalItemsPlacement =>
            _appSettings.HorizontalItemsPlacement == HorizontalAlignment.Right;

        public bool TopVerticalItemsPlacement =>
            _appSettings.VerticalItemsPlacement == VerticalAlignment.Top;
        public bool BottomVerticalItemsPlacement =>
            _appSettings.VerticalItemsPlacement == VerticalAlignment.Bottom;

        public ReactiveCommand<Unit, Unit> OpenResetDialogCommand { get; }
        public ReactiveCommand<Unit, Unit> UndoCommand { get; }
        public ReactiveCommand<Unit, Unit> RedoCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleShowItemCountsOnMapCommand { get; }
        public ReactiveCommand<string, Unit> SetLayoutOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetMapOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalItemsPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalItemsPlacementCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainWindow">
        /// The view-model of the main window.
        /// </param>
        /// <param name="appSettings">
        /// The app settings.
        /// </param>
        public TopMenuControlVM(MainWindowVM mainWindow, AppSettings appSettings)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }

            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            OpenResetDialogCommand = mainWindow.OpenResetDialogCommand;
            UndoCommand = mainWindow.UndoCommand;
            RedoCommand = mainWindow.RedoCommand;
            ToggleDisplayAllLocationsCommand = mainWindow.ToggleDisplayAllLocationsCommand;
            ToggleShowItemCountsOnMapCommand = ReactiveCommand.Create(ToggleShowItemCountsOnMap);
            SetLayoutOrientationCommand = ReactiveCommand.Create<string>(SetLayoutOrientation);
            SetMapOrientationCommand = ReactiveCommand.Create<string>(SetMapOrientation);
            SetHorizontalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalUIPanelPlacement);
            SetVerticalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetVerticalUIPanelPlacement);
            SetHorizontalItemsPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalItemsPlacement);
            SetVerticalItemsPlacementCommand = ReactiveCommand.Create<string>(SetVerticalItemsPlacement);

            _appSettings.PropertyChanged += OnAppSettingsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.DisplayAllLocations))
            {
                this.RaisePropertyChanged(nameof(DisplayAllLocations));
            }

            if (e.PropertyName == nameof(AppSettings.ShowItemCountsOnMap))
            {
                this.RaisePropertyChanged(nameof(ShowItemCountsOnMap));
            }

            if (e.PropertyName == nameof(AppSettings.LayoutOrientation))
            {
                UpdateLayoutOrientation();
            }

            if (e.PropertyName == nameof(AppSettings.MapOrientation))
            {
                UpdateMapOrientation();
            }

            if (e.PropertyName == nameof(AppSettings.HorizontalUIPanelPlacement))
            {
                UpdateHorizontalUIPanelPlacement();
            }

            if (e.PropertyName == nameof(AppSettings.VerticalUIPanelPlacement))
            {
                UpdateVerticalUIPanelPlacement();
            }

            if (e.PropertyName == nameof(AppSettings.HorizontalItemsPlacement))
            {
                UpdateHorizontalItemsPlacement();
            }

            if (e.PropertyName == nameof(AppSettings.VerticalItemsPlacement))
            {
                UpdateVerticalItemsPlacement();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelDock, DynamicLayoutOrientation,
        /// HorizontalLayoutOrientation, and VerticalLayoutOrientation properties.
        /// </summary>
        private void UpdateLayoutOrientation()
        {
            this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
            this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
            this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the MapPanelOrientation, DynamicMapOrientation,
        /// HorizontalMapOrientation, and VerticalMapOrientation properties.
        /// </summary>
        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(DynamicMapOrientation));
            this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
            this.RaisePropertyChanged(nameof(VerticalMapOrientation));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelDock, TopHorizontalUIPanelPlacement,
        /// and BottomHorizontalUIPanelPlacement properties.
        /// </summary>
        private void UpdateHorizontalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelDock, LeftVerticalUIPanelPlacement,
        /// and RightVerticalUIPanelPlacement properties.
        /// </summary>
        private void UpdateVerticalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelOrientationDock, ItemsPanelMargin,
        /// LeftHorizontalItemsPlacement, and RightHorizontalItemsPlacement properties.
        /// </summary>
        private void UpdateHorizontalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
            this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelOrientationDock, ItemsPanelMargin,
        /// TopVerticalItemsPlacement, and BottomVerticalItemsPlacement properties.
        /// </summary>
        private void UpdateVerticalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(TopVerticalItemsPlacement));
            this.RaisePropertyChanged(nameof(BottomVerticalItemsPlacement));
        }

        /// <summary>
        /// Toggles whether to show the item counts on the map.
        /// </summary>
        private void ToggleShowItemCountsOnMap()
        {
            _appSettings.ShowItemCountsOnMap = !_appSettings.ShowItemCountsOnMap;
        }

        /// <summary>
        /// Sets the layout orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new layout orientation value.
        /// </param>
        private void SetLayoutOrientation(string orientationString)
        {
            if (orientationString == "Dynamic")
            {
                _appSettings.LayoutOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                _appSettings.LayoutOrientation = orientation;
            }
        }

        /// <summary>
        /// Sets the map orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new map orientation value.
        /// </param>
        private void SetMapOrientation(string orientationString)
        {
            if (orientationString == "Dynamic")
            {
                _appSettings.MapOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                _appSettings.MapOrientation = orientation;
            }
        }

        /// <summary>
        /// Sets the horizontal UI panel orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new horizontal UI panel orientation value.
        /// </param>
        private void SetHorizontalUIPanelPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out VerticalAlignment orientation))
            {
                _appSettings.HorizontalUIPanelPlacement = orientation;
            }
        }

        /// <summary>
        /// Sets the vertical UI panel orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new vertical UI panel orientation value.
        /// </param>
        private void SetVerticalUIPanelPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out HorizontalAlignment orientation))
            {
                _appSettings.VerticalUIPanelPlacement = orientation;
            }
        }

        /// <summary>
        /// Sets the horizontal items placement orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new horizontal items placement orientation value.
        /// </param>
        private void SetHorizontalItemsPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out HorizontalAlignment orientation))
            {
                _appSettings.HorizontalItemsPlacement = orientation;
            }
        }

        /// <summary>
        /// Sets the vertical items placement orientation to the specified value.
        /// </summary>
        /// <param name="orientationString">
        /// A string representing the new vertical items placement orientation value.
        /// </param>
        private void SetVerticalItemsPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out VerticalAlignment orientation))
            {
                _appSettings.VerticalItemsPlacement = orientation;
            }
        }
    }
}
