using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
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
        public static bool DisplayAllLocations =>
            AppSettings.Instance.Tracker.DisplayAllLocations;
        public static bool ShowItemCountsOnMap =>
            AppSettings.Instance.Tracker.ShowItemCountsOnMap;

        public static bool DisplayMapsCompasses =>
            AppSettings.Instance.Layout.DisplayMapsCompasses;
        public static bool AlwaysDisplayDungeonItems =>
            AppSettings.Instance.Layout.AlwaysDisplayDungeonItems;

        public static bool DynamicLayoutOrientation =>
            AppSettings.Instance.Layout.LayoutOrientation == null;
        public static bool HorizontalLayoutOrientation =>
            AppSettings.Instance.Layout.LayoutOrientation == Orientation.Horizontal;
        public static bool VerticalLayoutOrientation =>
            AppSettings.Instance.Layout.LayoutOrientation == Orientation.Vertical;

        public static bool DynamicMapOrientation =>
            AppSettings.Instance.Layout.MapOrientation == null;
        public static bool HorizontalMapOrientation =>
            AppSettings.Instance.Layout.MapOrientation == Orientation.Horizontal;
        public static bool VerticalMapOrientation =>
            AppSettings.Instance.Layout.MapOrientation == Orientation.Vertical;

        public static bool TopHorizontalUIPanelPlacement =>
            AppSettings.Instance.Layout.HorizontalUIPanelPlacement == Dock.Top;
        public static bool BottomHorizontalUIPanelPlacement =>
            AppSettings.Instance.Layout.HorizontalUIPanelPlacement == Dock.Bottom;

        public static bool LeftVerticalUIPanelPlacement =>
            AppSettings.Instance.Layout.VerticalUIPanelPlacement == Dock.Left;
        public static bool RightVerticalUIPanelPlacement =>
            AppSettings.Instance.Layout.VerticalUIPanelPlacement == Dock.Right;

        public static bool LeftHorizontalItemsPlacement =>
            AppSettings.Instance.Layout.HorizontalItemsPlacement == Dock.Left;
        public static bool RightHorizontalItemsPlacement =>
            AppSettings.Instance.Layout.HorizontalItemsPlacement == Dock.Right;

        public static bool TopVerticalItemsPlacement =>
            AppSettings.Instance.Layout.VerticalItemsPlacement == Dock.Top;
        public static bool BottomVerticalItemsPlacement =>
            AppSettings.Instance.Layout.VerticalItemsPlacement == Dock.Bottom;

        public static bool OneHundredPercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.0;
        public static bool OneHundredTwentyFivePercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.25;
        public static bool OneHundredFiftyPercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.50;
        public static bool OneHundredSeventyFivePercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 1.75;
        public static bool TwoHundredPercentUIScale =>
            AppSettings.Instance.Layout.UIScale == 2.0;

        public ReactiveCommand<Unit, Unit> OpenResetDialogCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenAboutDialogCommand { get; }
        public ReactiveCommand<Unit, Unit> UndoCommand { get; }
        public ReactiveCommand<Unit, Unit> RedoCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleShowItemCountsOnMapCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayMapsCompassesCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleAlwaysDisplayDungeonItemsCommand { get; }
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

        private readonly ObservableAsPropertyHelper<bool> _isOpeningAboutDialog;
        public bool IsOpeningAboutDialog =>
            _isOpeningAboutDialog.Value;

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

            OpenAboutDialogCommand = ReactiveCommand.CreateFromObservable(OpenAboutDialogAsync);
            OpenAboutDialogCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningAboutDialog, out _isOpeningAboutDialog);

            UndoCommand = ReactiveCommand.Create(Undo, this.WhenAnyValue(x => x.CanUndo));
            RedoCommand = ReactiveCommand.Create(Redo, this.WhenAnyValue(x => x.CanRedo));
            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);

            ToggleShowItemCountsOnMapCommand = ReactiveCommand.Create(ToggleShowItemCountsOnMap);
            ToggleDisplayMapsCompassesCommand = ReactiveCommand.Create(ToggleDisplayMapsCompasses);
            ToggleAlwaysDisplayDungeonItemsCommand = ReactiveCommand.Create(ToggleAlwaysDisplayDungeonItems);
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
            if (e.PropertyName == nameof(LayoutSettings.DisplayMapsCompasses))
            {
                this.RaisePropertyChanged(nameof(DisplayMapsCompasses));
            }

            if (e.PropertyName == nameof(LayoutSettings.AlwaysDisplayDungeonItems))
            {
                this.RaisePropertyChanged(nameof(AlwaysDisplayDungeonItems));
            }

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
                this.RaisePropertyChanged(nameof(OneHundredPercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredTwentyFivePercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredFiftyPercentUIScale));
                this.RaisePropertyChanged(nameof(OneHundredSeventyFivePercentUIScale));
                this.RaisePropertyChanged(nameof(TwoHundredPercentUIScale));
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
        /// Toggles whether to display maps and compasses.
        /// </summary>
        private void ToggleDisplayMapsCompasses()
        {
            AppSettings.Instance.Layout.DisplayMapsCompasses =
                !AppSettings.Instance.Layout.DisplayMapsCompasses;
        }

        /// <summary>
        /// Toggles whether to always display dungeon items.
        /// </summary>
        private void ToggleAlwaysDisplayDungeonItems()
        {
            AppSettings.Instance.Layout.AlwaysDisplayDungeonItems =
                !AppSettings.Instance.Layout.AlwaysDisplayDungeonItems;
        }

        /// <summary>
        /// Resets the undo/redo manager, pinned locations, and game data to their starting values.
        /// </summary>
        private static void Reset()
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
        private static void OpenResetDialog()
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
        /// Opens an about dialog window.
        /// </summary>
        private static void OpenAboutDialog()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                bool? result = await App.DialogService.ShowDialog(new AboutDialogVM())
                    .ConfigureAwait(false);
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

        /// <summary>
        /// Returns the observable result of the OpenAboutDialog method.
        /// </summary>
        /// <returns>
        /// The observable result of the OpenAboutDialog method.
        /// </returns>
        private IObservable<Unit> OpenAboutDialogAsync()
        {
            return Observable.Start(() => { OpenAboutDialog(); });
        }
    }
}
