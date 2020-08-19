using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel class for the top menu control.
    /// </summary>
    public class TopMenuVM : ViewModelBase
    {
        public bool DisplayAllLocations =>
            AppSettings.Instance.Tracker.DisplayAllLocations;
        public bool ShowItemCountsOnMap =>
            AppSettings.Instance.Tracker.ShowItemCountsOnMap;

        public bool DynamicLayoutOrientation =>
            AppSettings.Instance.Layout.LayoutOrientation == null;
        public bool HorizontalLayoutOrientation =>
            AppSettings.Instance.Layout.LayoutOrientation == Orientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            AppSettings.Instance.Layout.LayoutOrientation == Orientation.Vertical;

        public bool DynamicMapOrientation =>
            AppSettings.Instance.Layout.MapOrientation == null;
        public bool HorizontalMapOrientation =>
            AppSettings.Instance.Layout.MapOrientation == Orientation.Horizontal;
        public bool VerticalMapOrientation =>
            AppSettings.Instance.Layout.MapOrientation == Orientation.Vertical;

        public bool TopHorizontalUIPanelPlacement =>
            AppSettings.Instance.Layout.HorizontalUIPanelPlacement == Dock.Top;
        public bool BottomHorizontalUIPanelPlacement =>
            AppSettings.Instance.Layout.HorizontalUIPanelPlacement == Dock.Bottom;

        public bool LeftVerticalUIPanelPlacement =>
            AppSettings.Instance.Layout.VerticalUIPanelPlacement == Dock.Left;
        public bool RightVerticalUIPanelPlacement =>
            AppSettings.Instance.Layout.VerticalUIPanelPlacement == Dock.Right;

        public bool LeftHorizontalItemsPlacement =>
            AppSettings.Instance.Layout.HorizontalItemsPlacement == Dock.Left;
        public bool RightHorizontalItemsPlacement =>
            AppSettings.Instance.Layout.HorizontalItemsPlacement == Dock.Right;

        public bool TopVerticalItemsPlacement =>
            AppSettings.Instance.Layout.VerticalItemsPlacement == Dock.Top;
        public bool BottomVerticalItemsPlacement =>
            AppSettings.Instance.Layout.VerticalItemsPlacement == Dock.Bottom;

        public bool NoneUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.0;
        public bool TwentyFivePercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.25;
        public bool FiftyPercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.50;
        public bool SeventyFivePercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.75;
        public bool OneHundredPercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 2.0;

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
        public ReactiveCommand<string, Unit> SetUIScaleCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isOpeningResetDialog;
        public bool IsOpeningResetDialog =>
            _isOpeningResetDialog.Value;

        private bool _canUndo;
        public bool CanUndo
        {
            get => _canUndo;
            private set => this.RaiseAndSetIfChanged(ref _canUndo, value);
        }

        private bool _canRedo;
        public bool CanRedo
        {
            get => _canRedo;
            private set => this.RaiseAndSetIfChanged(ref _canRedo, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TopMenuVM()
        {
            OpenResetDialogCommand = ReactiveCommand.CreateFromObservable(OpenResetDialogAsync);
            OpenResetDialogCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningResetDialog, out _isOpeningResetDialog);

            UndoCommand = ReactiveCommand.Create(Undo, this.WhenAnyValue(x => x.CanUndo));
            RedoCommand = ReactiveCommand.Create(Redo, this.WhenAnyValue(x => x.CanRedo));
            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);

            ToggleShowItemCountsOnMapCommand = ReactiveCommand.Create(ToggleShowItemCountsOnMap);
            SetLayoutOrientationCommand = ReactiveCommand.Create<string>(SetLayoutOrientation);
            SetMapOrientationCommand = ReactiveCommand.Create<string>(SetMapOrientation);
            SetHorizontalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalUIPanelPlacement);
            SetVerticalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetVerticalUIPanelPlacement);
            SetHorizontalItemsPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalItemsPlacement);
            SetVerticalItemsPlacementCommand = ReactiveCommand.Create<string>(SetVerticalItemsPlacement);
            SetUIScaleCommand = ReactiveCommand.Create<string>(SetUIScale);

            UndoRedoManager.Instance.UndoableActions.CollectionChanged += OnUndoChanged;
            UndoRedoManager.Instance.RedoableActions.CollectionChanged += OnRedoChanged;
            AppSettings.Instance.Tracker.PropertyChanged += OnTrackerSettingsChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the observable stack of undoable actions.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnUndoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanUndo();
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the observable stack of redoable actions.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRedoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanRedo();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the TrackerSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnTrackerSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TrackerSettings.DisplayAllLocations))
            {
                this.RaisePropertyChanged(nameof(DisplayAllLocations));
            }

            if (e.PropertyName == nameof(TrackerSettings.ShowItemCountsOnMap))
            {
                this.RaisePropertyChanged(nameof(ShowItemCountsOnMap));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.LayoutOrientation))
            {
                this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
                this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
                this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
            }

            if (e.PropertyName == nameof(LayoutSettings.MapOrientation))
            {
                this.RaisePropertyChanged(nameof(DynamicMapOrientation));
                this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
                this.RaisePropertyChanged(nameof(VerticalMapOrientation));
            }

            if (e.PropertyName == nameof(LayoutSettings.HorizontalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
                this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.VerticalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
                this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.HorizontalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
                this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.VerticalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(TopVerticalItemsPlacement));
                this.RaisePropertyChanged(nameof(BottomVerticalItemsPlacement));
            }

            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(NoneUIScale));
                this.RaisePropertyChanged(nameof(TwentyFivePercentUIScale));
                this.RaisePropertyChanged(nameof(FiftyPercentUIScale));
                this.RaisePropertyChanged(nameof(SeventyFivePercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredPercentUIScale));
            }
        }

        /// <summary>
        /// Updates the CanUndo property with a value representing whether there are objects in the
        /// Undoable stack.
        /// </summary>
        private void UpdateCanUndo()
        {
            CanUndo = UndoRedoManager.Instance.CanUndo();
        }

        /// <summary>
        /// Undoes the last action.
        /// </summary>
        private void Undo()
        {
            UndoRedoManager.Instance.Undo();
        }

        /// <summary>
        /// Updates the CanRedo property with a value representing whether there are objects in the
        /// Redoable stack.
        /// </summary>
        private void UpdateCanRedo()
        {
            CanRedo = UndoRedoManager.Instance.CanRedo();
        }

        /// <summary>
        /// Redoes the last action.
        /// </summary>
        private void Redo()
        {
            UndoRedoManager.Instance.Redo();
        }

        /// <summary>
        /// Toggles whether to show the item counts on the map.
        /// </summary>
        private void ToggleShowItemCountsOnMap()
        {
            AppSettings.Instance.Tracker.ShowItemCountsOnMap =
                !AppSettings.Instance.Tracker.ShowItemCountsOnMap;
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
                AppSettings.Instance.Layout.LayoutOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                AppSettings.Instance.Layout.LayoutOrientation = orientation;
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
                AppSettings.Instance.Layout.MapOrientation = null;
            }
            else if (Enum.TryParse(orientationString, out Orientation orientation))
            {
                AppSettings.Instance.Layout.MapOrientation = orientation;
            }
        }

        /// <summary>
        /// Sets the horizontal UI panel orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new horizontal UI panel orientation value.
        /// </param>
        private void SetHorizontalUIPanelPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                AppSettings.Instance.Layout.HorizontalUIPanelPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the vertical UI panel orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new vertical UI panel orientation value.
        /// </param>
        private void SetVerticalUIPanelPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                AppSettings.Instance.Layout.VerticalUIPanelPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the horizontal items placement orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new horizontal items placement orientation value.
        /// </param>
        private void SetHorizontalItemsPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                AppSettings.Instance.Layout.HorizontalItemsPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the vertical items placement orientation to the specified value.
        /// </summary>
        /// <param name="dockString">
        /// A string representing the new vertical items placement orientation value.
        /// </param>
        private void SetVerticalItemsPlacement(string dockString)
        {
            if (Enum.TryParse(dockString, out Dock dock))
            {
                AppSettings.Instance.Layout.VerticalItemsPlacement = dock;
            }
        }

        /// <summary>
        /// Sets the UI scale to the specified value.
        /// </summary>
        /// <param name="uiScaleValue">
        /// A floating point number representing the UI scale value.
        /// </param>
        private static void SetUIScale(string uiScaleValue)
        {
            AppSettings.Instance.Layout.UIScale = double.Parse(uiScaleValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Toggles whether to display all locations on the map.
        /// </summary>
        private void ToggleDisplayAllLocations()
        {
            AppSettings.Instance.Tracker.DisplayAllLocations =
                !AppSettings.Instance.Tracker.DisplayAllLocations;
        }

        /// <summary>
        /// Resets the undo/redo manager, pinned locations, and game data to their starting values.
        /// </summary>
        private void Reset()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                UndoRedoManager.Instance.Reset();
                PinnedLocationCollection.Instance.Clear();
                AutoTracker.Instance.Stop();
                BossPlacementDictionary.Instance.Reset();
                LocationDictionary.Instance.Reset();
                PrizePlacementDictionary.Instance.Reset();
                ItemDictionary.Instance.Reset();
                ConnectionCollection.Instance.Clear();
            });
        }

        /// <summary>
        /// Opens a reset dialog window.
        /// </summary>
        private void OpenResetDialog()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                bool? result = await App.DialogService.ShowDialog(
                    new MessageBoxDialogVM("Warning",
                    "Resetting the tracker will set all items and locations back to their " +
                    "starting values. This cannot be undone.\n\nDo you wish to proceed?"))
                    .ConfigureAwait(false);

                if (result.HasValue && result.Value)
                {
                    Reset();
                }
            });
        }

        /// <summary>
        /// Returns the observable result of the OpenResetDialog method.
        /// </summary>
        /// <returns>
        /// The observable result of the OpenResetDialog method.
        /// </returns>
        private IObservable<Unit> OpenResetDialogAsync()
        {
            return Observable.Start(() => { OpenResetDialog(); });
        }
    }
}
