using Avalonia.Layout;
using OpenTracker.Models;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel class for the top menu control.
    /// </summary>
    public class TopMenuVM : ViewModelBase
    {
        public bool DisplayAllLocations =>
            AppSettings.Instance.DisplayAllLocations;
        public bool ShowItemCountsOnMap =>
            AppSettings.Instance.ShowItemCountsOnMap;

        public bool DynamicLayoutOrientation =>
            AppSettings.Instance.LayoutOrientation == null;
        public bool HorizontalLayoutOrientation =>
            AppSettings.Instance.LayoutOrientation == Orientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            AppSettings.Instance.LayoutOrientation == Orientation.Vertical;

        public bool DynamicMapOrientation =>
            AppSettings.Instance.MapOrientation == null;
        public bool HorizontalMapOrientation =>
            AppSettings.Instance.MapOrientation == Orientation.Horizontal;
        public bool VerticalMapOrientation =>
            AppSettings.Instance.MapOrientation == Orientation.Vertical;

        public bool TopHorizontalUIPanelPlacement =>
            AppSettings.Instance.HorizontalUIPanelPlacement == VerticalAlignment.Top;
        public bool BottomHorizontalUIPanelPlacement =>
            AppSettings.Instance.HorizontalUIPanelPlacement == VerticalAlignment.Bottom;

        public bool LeftVerticalUIPanelPlacement =>
            AppSettings.Instance.VerticalUIPanelPlacement == HorizontalAlignment.Left;
        public bool RightVerticalUIPanelPlacement =>
            AppSettings.Instance.VerticalUIPanelPlacement == HorizontalAlignment.Right;

        public bool LeftHorizontalItemsPlacement =>
            AppSettings.Instance.HorizontalItemsPlacement == HorizontalAlignment.Left;
        public bool RightHorizontalItemsPlacement =>
            AppSettings.Instance.HorizontalItemsPlacement == HorizontalAlignment.Right;

        public bool TopVerticalItemsPlacement =>
            AppSettings.Instance.VerticalItemsPlacement == VerticalAlignment.Top;
        public bool BottomVerticalItemsPlacement =>
            AppSettings.Instance.VerticalItemsPlacement == VerticalAlignment.Bottom;

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
        public TopMenuVM(MainWindowVM mainWindow)
        {
            if (mainWindow == null)
            {
                throw new ArgumentNullException(nameof(mainWindow));
            }

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

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
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
                this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
                this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
                this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
            }

            if (e.PropertyName == nameof(AppSettings.MapOrientation))
            {
                this.RaisePropertyChanged(nameof(DynamicMapOrientation));
                this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
                this.RaisePropertyChanged(nameof(VerticalMapOrientation));
            }

            if (e.PropertyName == nameof(AppSettings.HorizontalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
                this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
            }

            if (e.PropertyName == nameof(AppSettings.VerticalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
                this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
            }

            if (e.PropertyName == nameof(AppSettings.HorizontalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
                this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
            }

            if (e.PropertyName == nameof(AppSettings.VerticalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(TopVerticalItemsPlacement));
                this.RaisePropertyChanged(nameof(BottomVerticalItemsPlacement));
            }
        }

        /// <summary>
        /// Toggles whether to show the item counts on the map.
        /// </summary>
        private void ToggleShowItemCountsOnMap()
        {
            AppSettings.Instance.ShowItemCountsOnMap = !AppSettings.Instance.ShowItemCountsOnMap;
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
                AppSettings.Instance.LayoutOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                AppSettings.Instance.LayoutOrientation = orientation;
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
                AppSettings.Instance.MapOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                AppSettings.Instance.MapOrientation = orientation;
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
                AppSettings.Instance.HorizontalUIPanelPlacement = orientation;
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
                AppSettings.Instance.VerticalUIPanelPlacement = orientation;
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
                AppSettings.Instance.HorizontalItemsPlacement = orientation;
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
                AppSettings.Instance.VerticalItemsPlacement = orientation;
            }
        }
    }
}
